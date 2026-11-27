#!/usr/bin/env python3
"""
RoboHelp (HelpSource/) のヘルプ資産を ASP.NET Core Razor Pages に移植するための変換ツール。

生成物:
  Pages/Topics/**/<Topic>.cshtml / .cshtml.cs   各トピックページ (クリーンなマークアップ)
  Data/toc.json        目次 (.hhc を構造化したもの)
  Data/keywords.json   索引 (.hhk を構造化したもの)
  Data/topics.json     トピック一覧 (タイトル・本文テキスト。全文検索用)
  Data/helpoptions.json eHelp.xml のオプション
  wwwroot/images/help/  画像

使い方:  python3 tools/convert_help.py
"""
import json
import os
import re
import shutil
import sys
import xml.etree.ElementTree as ET
from pathlib import Path, PurePosixPath

from bs4 import BeautifulSoup, Comment, NavigableString, Tag

ROOT = Path(__file__).resolve().parent.parent
SRC = ROOT / "HelpSource"
HTMLDIR = SRC / "HTMLFILE"
PAGES = ROOT / "Pages" / "Topics"
DATA = ROOT / "Data"
IMGOUT = ROOT / "wwwroot" / "images" / "help"
NS_ROOT = "rici7Sch.Pages.Topics"
JA_DIR = SRC / "ja"                       # 日本語訳 (英語→日本語の対訳辞書, *.json)
UNTRANSLATED = ROOT / "tools" / "untranslated.json"

# ---------------------------------------------------------------- 日本語訳
TRANS: dict[str, str] = {}
MISSING: dict[str, str] = {}               # 未翻訳の英語 → 最初に出現したトピック
BLOCKS = ["p", "li", "td", "th", "h2", "h3", "h4", "caption"]
NESTED = ["p", "li", "ul", "ol", "div", "table"]


def tkey(s: str) -> str:
    return re.sub(r"\s+", " ", s).strip()


def load_translations():
    if JA_DIR.exists():
        for f in sorted(JA_DIR.glob("*.json")):
            for k, v in json.loads(f.read_text(encoding="utf-8")).items():
                TRANS[tkey(k)] = v


def tr(s: str, ctx: str) -> str:
    """見出しやタイトルなどの文字列を訳す。訳がなければ英語のまま (未翻訳として記録)。"""
    k = tkey(s)
    if not k or not re.search("[A-Za-z]", k):
        return s
    if k in TRANS:
        return TRANS[k]
    MISSING.setdefault(k, ctx)
    return s


def translate_blocks(body, ctx: str):
    """段落・リスト項目などの末端ブロックの中身 (インラインのタグ込み) を訳文に置き換える。"""
    for el in body.find_all(BLOCKS):
        if el.find(NESTED):
            continue
        html = el.decode_contents()
        k = tkey(html)
        if not re.search("[A-Za-z]", BeautifulSoup(k, "html.parser").get_text()):
            continue
        if k in TRANS:
            el.clear()
            el.append(BeautifulSoup(TRANS[k], "html.parser"))
        elif translate_terms_only(el) or translate_links_only(el):
            pass
        else:
            MISSING.setdefault(k, ctx)


def translate_links_only(el) -> bool:
    """リンク (他トピックへの参照) だけのブロックは、リンク文字列をトピック名の訳で置き換える。"""
    links = el.find_all("a")
    if not links:
        return False
    outside = "".join(t for t in el.find_all(string=True) if not t.find_parent("a"))
    if re.search("[A-Za-z]", outside):
        return False
    keys = [tkey(a.get_text(" ")) for a in links]
    if not all(k in TRANS for k in keys):
        return False
    for a, k in zip(links, keys):
        a.clear()
        a.append(BeautifulSoup(TRANS[k], "html.parser"))
    return True


def translate_terms_only(el) -> bool:
    """画面項目名 (span.Term-*) だけで構成されたブロック (例: 見出し) は項目名ごとに訳す。"""
    spans = el.find_all("span", class_=re.compile(r"^Term-"))
    if not spans:
        return False
    outside = "".join(t for t in el.find_all(string=True) if not t.find_parent("span"))
    if re.search("[A-Za-z]", outside):
        return False
    keys = [tkey(sp.decode_contents()) for sp in spans]
    if not all(k in TRANS for k in keys):
        return False
    for sp, k in zip(spans, keys):
        sp.clear()
        sp.append(BeautifulSoup(TRANS[k], "html.parser"))
    return True


# 実ファイル名 (大文字小文字を正規化して解決するため)
ALL_TOPICS = {}  # lower id -> real id (e.g. "rpcs/idh_how_creep" -> "RPCS/IDH_HOW_CREEP")
for p in sorted(HTMLDIR.rglob("*.htm")):
    tid = p.relative_to(HTMLDIR).with_suffix("").as_posix()
    ALL_TOPICS[tid.lower()] = tid

warnings = []


def read_html(path: Path) -> str:
    return path.read_bytes().decode("cp1252", errors="replace")


def resolve_topic(href: str, base_id: str | None):
    """'../RPCS/X.htm#a' のような参照をトピックIDとアンカーに解決する。"""
    href = href.strip()
    path, _, anchor = href.partition("#")
    if not path:
        return base_id, anchor
    if base_id is None:
        rel = PurePosixPath(path)
        parts = list(rel.parts)
        if parts and parts[0].upper() == "HTMLFILE":
            parts = parts[1:]
        joined = "/".join(parts)
    else:
        base_dir = PurePosixPath(base_id).parent
        joined = os.path.normpath((base_dir / path).as_posix()).replace("\\", "/")
    joined = re.sub(r"\.html?$", "", joined, flags=re.I)
    real = ALL_TOPICS.get(joined.lower())
    if real is None:
        base = PurePosixPath(joined).name.lower()
        cands = [v for k, v in ALL_TOPICS.items() if PurePosixPath(k).name == base]
        if len(cands) == 1:
            real = cands[0]
            warnings.append(f"link {href!r} from {base_id} resolved by file name to {real}")
    if real is None:
        warnings.append(f"unresolved link {href!r} from {base_id}")
    return real, anchor


def topic_url(tid: str, anchor: str = "") -> str:
    return f"~/Topics/{tid}" + (f"#{anchor}" if anchor else "")


def class_name(tid: str) -> str:
    name = PurePosixPath(tid).name
    name = re.sub(r"[^A-Za-z0-9_]", "_", name)
    return f"{name}Model"


def namespace(tid: str) -> str:
    parent = PurePosixPath(tid).parent.as_posix()
    if parent == ".":
        return NS_ROOT
    return NS_ROOT + "." + ".".join(re.sub(r"[^A-Za-z0-9_]", "_", s) for s in parent.split("/"))


# ---------------------------------------------------------------- 目次 / 索引
def parse_object(obj_html: str):
    params = re.findall(r'<param\s+name="([^"]+)"\s+value="([^"]*)"', obj_html, flags=re.I)
    return params


def build_toc():
    """hhc を <ul>/</ul>/<object> のトークン列として読み、ツリーに変換する。"""
    hhc = next(SRC.glob("*.hhc"))
    raw = read_html(hhc)
    raw = raw[raw.lower().index("</object>") + 9:]  # site properties を飛ばす
    root = {"children": []}
    stack = [root]
    last = None
    for m in re.finditer(r"<ul>|</ul>|<object[^>]*>.*?</object>", raw, flags=re.I | re.S):
        tok = m.group(0)
        low = tok.lower()
        if low == "<ul>":
            if last is not None:
                stack.append(last)
        elif low == "</ul>":
            if len(stack) > 1:
                stack.pop()
            last = None
        else:
            params = parse_object(tok)
            name = next((v for n, v in params if n == "Name"), "")
            local = next((v for n, v in params if n == "Local"), None)
            en = re.sub(r"\s+", " ", name).strip()
            node = {"title": tr(en, "toc"), "titleEn": en, "children": []}
            if local:
                tid, anchor = resolve_topic(local, None)
                if tid:
                    node["topic"] = tid
                    if anchor:
                        node["anchor"] = anchor
            stack[-1]["children"].append(node)
            last = node
    # 最上位の <ul> の分を補正
    return root["children"]


def build_keywords():
    hhk = next(SRC.glob("*.hhk"))
    soup = BeautifulSoup(read_html(hhk), "html.parser")
    entries = []
    for obj in soup.find_all("object", attrs={"type": "text/sitemap"}):
        params = [(p.get("name"), p.get("value")) for p in obj.find_all("param")]
        if not params:
            continue
        keyword = re.sub(r"\s+", " ", params[0][1]).strip()
        topics = []
        rest = params[1:]
        i = 0
        while i < len(rest):
            n, v = rest[i]
            if n == "Name" and i + 1 < len(rest) and rest[i + 1][0] == "Local":
                tid, anchor = resolve_topic(rest[i + 1][1], None)
                if tid:
                    en = re.sub(r"\s+", " ", v).strip()
                    topics.append({"title": tr(en, "index"), "titleEn": en, "topic": tid, **({"anchor": anchor} if anchor else {})})
                i += 2
            elif n == "Local":
                tid, anchor = resolve_topic(v, None)
                if tid:
                    topics.append({"title": tr(keyword, "index"), "titleEn": keyword, "topic": tid, **({"anchor": anchor} if anchor else {})})
                i += 1
            else:
                i += 1
        entries.append({"keyword": tr(keyword, "index"), "keywordEn": keyword, "topics": topics})
    entries.sort(key=lambda e: e["keywordEn"].lower())
    return entries


def build_options():
    raw = (SRC / "eHelp.xml").read_text(encoding="utf-8", errors="replace")
    # 先頭に複数ルートがあるため、ラップしてからパース
    body = re.sub(r"<\?xml[^>]*\?>|<!DOCTYPE[^>]*>", "", raw)
    root = ET.fromstring(f"<root>{body}</root>")
    opts = root.find("EHELP-OPTIONS/OPTIONS")
    label = (root.findtext("EHELP-OPTIONS/BUTTON-LABEL") or "").strip()
    yes = lambda k: (opts.get(k, "NO").upper() == "YES")
    return {
        "webSearch": {
            "enabled": yes("ENABLED"),
            "embedded": yes("EMBEDDED"),
            "mainWindowOnly": yes("MAIN-WINDOW-ONLY"),
            "buttonLabel": label or "WebSearch",
        }
    }


# ---------------------------------------------------------------- トピック変換
# 元の class 名 (mshtmlExpNonbrand.css / mshtmlExp2nd.css のセレクタ) をそのまま残す
P_CLASSES = {c.lower(): c for c in (
    "T1", "T2", "T3", "T4", "Note", "Note-indent", "Note-ol",
    "Normal-indent", "Normal-ol", "footer", "Graphic")}
P_CLASSES["normal-indent"] = "Normal-indent"
P_CLASSES["indent"] = "Normal-indent"
SPAN_CLASSES = {c.lower(): c for c in ("Term-Soft", "Term-OS", "Term-Hard")}
HEADING_LEVEL = {"T1": "2", "T2": "3", "T3": "4", "T4": "4"}


def new_tag(soup, name, **attrs):
    t = soup.new_tag(name)
    for k, v in attrs.items():
        t[k.replace("_", "-") if k != "class_" else "class"] = v
    return t


def convert_links(soup, root, tid):
    for a in list(root.find_all("a")):
        href = a.get("href", "")
        if "kadovTextPopup" in href:
            continue
        m = re.search(r"BSSCPopup\('([^']+)'\)", href)
        if m:
            ref, anchor = resolve_topic(m.group(1), tid)
            a.attrs = {"href": topic_url(ref, anchor) if ref else "#", "class": "help-popup-link",
                       "data-help-popup": "true"}
        elif href.startswith(("http:", "https:", "mailto:")):
            a.attrs = {"href": href, "target": "_blank", "rel": "noopener"}
        elif href:
            ref, anchor = resolve_topic(href, tid)
            if ref:
                a.attrs = {"href": topic_url(ref, anchor)}
            else:
                a.unwrap()
        else:
            a.unwrap()


def convert_topic(path: Path):
    tid = path.relative_to(HTMLDIR).with_suffix("").as_posix()
    soup = BeautifulSoup(read_html(path), "html.parser")
    title = (soup.title.get_text(" ", strip=True) if soup.title else PurePosixPath(tid).name)
    body = soup.body or soup
    sheet = "mshtmlExpNonbrand"
    for link in soup.find_all("link", rel=True):
        name = PurePosixPath(link.get("href", "")).stem
        if name.startswith("mshtmlExp"):
            sheet = name

    # コメント・スクリプトを除去
    for c in body.find_all(string=lambda s: isinstance(s, Comment)):
        c.extract()
    for s in body.find_all(["script", "noscript", "style"]):
        s.decompose()

    # 通常リンク・ファイルポップアップを先に変換 (ドロップダウン内も対象)
    convert_links(soup, body, tid)

    # ドロップダウン (kadov text popup) の内容を退避
    popups = {}
    for div in body.find_all("div", class_="x-popup-text"):
        popups[div.get("id")] = div.extract()

    h1 = body.find("h1")
    heading = h1.get_text(" ", strip=True) if h1 else title
    if h1:
        h1.decompose()

    # リンク
    counter = 0
    for a in list(body.find_all("a")):
        href = a.get("href", "")
        if "kadovTextPopup" in href:
            counter += 1
            pid = (a.get("x-use-popup") or "").lstrip("#")
            content = popups.get(pid)
            target = f"dropdown-{counter}"
            btn = new_tag(soup, "button", type="button", class_="help-dropdown-toggle",
                          data_bs_toggle="collapse", data_bs_target=f"#{target}",
                          aria_expanded="false", aria_controls=target)
            btn.extend(list(a.contents))
            a.replace_with(btn)
            if content is not None:
                panel = new_tag(soup, "div", class_="collapse help-dropdown", id=target)
                panel.extend(list(content.contents))
                block = btn.find_parent(["p", "li"])
                if block and block.name == "p":
                    block.insert_after(panel)
                elif block:
                    block.append(panel)
                else:
                    btn.insert_after(panel)
            else:
                warnings.append(f"missing popup {pid} in {tid}")
            continue

    # 画像
    for img in body.find_all("img"):
        src = img.get("src", "")
        name = PurePosixPath(src).name
        if not (SRC / "image" / name).exists():
            warnings.append(f"missing image {src} in {tid}")
        new = {"src": f"~/images/help/{name}", "alt": "", "loading": "lazy"}
        for k in ("width", "height"):
            if img.get(k):
                new[k] = img[k]
        img.attrs = new
        # アイコン的な小画像はインライン、それ以外は図として扱う
        try:
            if int(new.get("width", 0)) > 40 or int(new.get("height", 0)) > 40:
                new["class"] = "help-img"
            else:
                new["class"] = "help-icon"
        except ValueError:
            new["class"] = "help-img"

    # 要素ごとのマッピング
    for el in list(body.find_all(True)):
        if el.parent is None:
            continue
        cls_attr = el.get("class") or []
        if isinstance(cls_attr, str):
            cls_attr = cls_attr.split()
        classes = [c.lower() for c in cls_attr]
        keep_id = el.get("id")
        if el.name in ("img", "button") or (el.name == "a") or "help-dropdown" in classes:
            continue
        if el.name == "p":
            mapped = [P_CLASSES[c] for c in classes if c in P_CLASSES]
            el.attrs = {"class": mapped[0]} if mapped else {}
            if mapped and mapped[0] in HEADING_LEVEL:
                el["role"] = "heading"
                el["aria-level"] = HEADING_LEVEL[mapped[0]]
        elif el.name == "span":
            mapped = [SPAN_CLASSES[c] for c in classes if c in SPAN_CLASSES]
            if mapped:
                el.attrs = {"class": mapped[0]}
            else:
                el.unwrap()
                continue
        elif el.name == "table":
            el.attrs = {"class": "table table-sm table-bordered help-table"}
        elif el.name in ("td", "th"):
            el.attrs = {k: v for k, v in el.attrs.items() if k in ("colspan", "rowspan")}
        elif el.name == "ol":
            el.attrs = {"start": el["start"]} if el.get("start") else {}
        elif el.name == "div" and "indent" in classes:
            el.attrs = {"class": "help-step-extra"}
        elif el.name == "div" and keep_id == "table":
            el.attrs = {"class": "table-responsive", "id": "table"}
            continue
        elif el.name in ("font", "div") and not classes:
            el.unwrap()
            continue
        else:
            el.attrs = {}
        if keep_id and el.name in ("h2", "h3", "h4", "p", "li", "ol", "ul", "table"):
            el["id"] = keep_id

    # 空段落の除去
    for p in body.find_all("p"):
        if not p.get_text(strip=True) and not p.find(["img", "button"]):
            p.decompose()

    # body 直下の地の文 (タグなしのテキスト) を段落にする
    for node in list(body.children):
        if isinstance(node, NavigableString) and not isinstance(node, Comment) and node.strip():
            node.wrap(soup.new_tag("p"))

    text_en = re.sub(r"\s+", " ", body.get_text(" ", strip=True))
    translate_blocks(body, tid)

    inner = "".join(str(c) for c in body.contents)
    inner = re.sub(r"[ \t]*\n\s*\n+", "\n", inner)
    inner = re.sub(r"[ \t]+", " ", inner).strip()

    text = BeautifulSoup(inner, "html.parser").get_text(" ", strip=True)
    text = re.sub(r"\s+", " ", text)
    norm = lambda x: re.sub(r"\s+", " ", x).strip()
    return tid, norm(title), norm(heading), inner, text, text_en, sheet


def razor_escape(html: str) -> str:
    return html.replace("@", "@@")


def write_page(tid, title, heading, inner):
    cshtml = PAGES / f"{tid}.cshtml"
    cshtml.parent.mkdir(parents=True, exist_ok=True)
    ns, cls = namespace(tid), class_name(tid)
    cshtml.write_text(
        "@page\n"
        f"@model {ns}.{cls}\n"
        "@* このファイルは tools/convert_help.py により生成されています。 *@\n"
        "\n"
        + razor_escape(inner) + "\n",
        encoding="utf-8",
    )
    (PAGES / f"{tid}.cshtml.cs").write_text(
        "// このファイルは tools/convert_help.py により生成されています。\n"
        f"namespace {ns};\n\n"
        f"public class {cls} : TopicPageModel\n"
        "{\n"
        f"    public override string TopicId => \"{tid}\";\n"
        "}\n",
        encoding="utf-8",
    )


def main():
    # 生成済みトピックを削除 (共通ファイルは残す)
    keep = {"_ViewStart.cshtml", "_ViewImports.cshtml", "_TopicLayout.cshtml", "TopicPageModel.cs"}
    if PAGES.exists():
        for f in PAGES.rglob("*"):
            if f.is_file() and f.name not in keep:
                f.unlink()
        for d in sorted((d for d in PAGES.rglob("*") if d.is_dir()), reverse=True):
            if not any(d.iterdir()):
                d.rmdir()
    PAGES.mkdir(parents=True, exist_ok=True)
    DATA.mkdir(exist_ok=True)

    load_translations()
    topics = []
    for path in sorted(HTMLDIR.rglob("*.htm")):
        tid, title, heading, inner, text, text_en, sheet = convert_topic(path)
        write_page(tid, title, heading, inner)
        en = heading or title
        # 検索は日本語・英語のどちらでもヒットするよう両方を持つ
        topics.append({"id": tid, "title": tr(en, tid), "titleEn": en, "stylesheet": sheet,
                       "text": text if text == text_en else f"{text} {text_en}"})

    toc = build_toc()
    keywords = build_keywords()

    dump = lambda name, obj: (DATA / name).write_text(json.dumps(obj, ensure_ascii=False, indent=1), encoding="utf-8")
    dump("toc.json", toc)
    dump("keywords.json", keywords)
    dump("topics.json", topics)
    dump("helpoptions.json", build_options())

    IMGOUT.mkdir(parents=True, exist_ok=True)
    for img in (SRC / "image").glob("*.gif"):
        shutil.copy2(img, IMGOUT / img.name)
    # mshtmlExp*.css が url(t3.gif) で参照するため CSS と同じ場所に置く
    shutil.copy2(SRC / "t3.gif", ROOT / "wwwroot" / "css" / "t3.gif")

    # 未翻訳の一覧 (翻訳作業用)
    UNTRANSLATED.write_text(json.dumps(MISSING, ensure_ascii=False, indent=1), encoding="utf-8")

    print(f"topics: {len(topics)}  toc roots: {len(toc)}  keywords: {len(keywords)}")
    print(f"translations: {len(TRANS)}  untranslated: {len(MISSING)} (tools/untranslated.json)")
    for w in sorted(set(warnings)):
        print("WARN", w, file=sys.stderr)


if __name__ == "__main__":
    main()
