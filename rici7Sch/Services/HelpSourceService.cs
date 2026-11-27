using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace rici7Sch.Services
{
    /// <summary>HelpSource 内のファイル情報。</summary>
    public sealed record HelpSourceFile(string Name, string RelativePath, long Size, string Kind, string Role, string UsedBy);

    /// <summary>画像と、それを使っているトピック。</summary>
    public sealed record HelpImage(string Name, string Url, long Size, IReadOnlyList<TopicInfo> UsedBy);

    /// <summary>用語集 (.glo) の用語。</summary>
    public sealed record GlossaryTerm(string Term, string Definition);

    /// <summary>ブラウズシーケンス (.brs)。</summary>
    public sealed record BrowseSequence(string Name, IReadOnlyList<string> Topics);

    /// <summary>
    /// 移植元の RoboHelp 資産 (HelpSource/) を実行時に読み込むサービス。
    /// RoboHHRE.lng (UI 文言)・.brs・.glo・eHelp.xml・画像・元の HTML トピックを扱う。
    /// </summary>
    public sealed class HelpSourceService
    {
        private static readonly Regex AssetRef = new(
            "(?<attr>href|src)=\"(?:\\.\\./)+(?<path>mshtmlExp[^\"]*\\.css|ehlpdhtm\\.js|image/[^\"]+)\"",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private readonly HelpCatalog _catalog;
        private readonly Dictionary<string, string> _htmlFiles;           // ID → 物理パス
        private readonly Encoding _sourceEncoding;

        public string Root { get; }
        public string HtmlRoot { get; }

        /// <summary>UI 文言: RoboHHRE.lng (英語) に RoboHHRE.ja.lng (日本語) を重ねたもの。[セクション][キー] = 文言</summary>
        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> Strings { get; }
        /// <summary>RoboHHRE.lng (元の英語の文言)。</summary>
        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> OriginalStrings { get; }
        /// <summary>RoboHHRE.ja.lng (日本語訳)。</summary>
        public IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> JapaneseStrings { get; }
        public IReadOnlyList<GlossaryTerm> Glossary { get; }
        public IReadOnlyList<BrowseSequence> BrowseSequences { get; }
        public string? FirstPage { get; }

        public HelpSourceService(IWebHostEnvironment env, HelpCatalog catalog)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _sourceEncoding = Encoding.GetEncoding(1252);
            _catalog = catalog;
            Root = Path.Combine(env.ContentRootPath, "HelpSource");
            HtmlRoot = Path.Combine(Root, "HTMLFILE");

            _htmlFiles = Directory.Exists(HtmlRoot)
                ? Directory.EnumerateFiles(HtmlRoot, "*.htm", SearchOption.AllDirectories)
                    .ToDictionary(
                        f => Path.ChangeExtension(Path.GetRelativePath(HtmlRoot, f), null).Replace('\\', '/'),
                        f => f, StringComparer.OrdinalIgnoreCase)
                : new(StringComparer.OrdinalIgnoreCase);

            var lngFiles = Directory.Exists(Root) ? Directory.GetFiles(Root, "*.lng") : [];
            OriginalStrings = LoadLng(lngFiles.FirstOrDefault(f => !f.EndsWith(".ja.lng", StringComparison.OrdinalIgnoreCase)));
            JapaneseStrings = LoadLng(lngFiles.FirstOrDefault(f => f.EndsWith(".ja.lng", StringComparison.OrdinalIgnoreCase)));
            Strings = Merge(OriginalStrings, JapaneseStrings);
            Glossary = LoadGlossary(FindFile("*.glo"));
            BrowseSequences = LoadBrowseSequences(FindFile("*.brs"));
            FirstPage = LoadFirstPage(Path.Combine(Root, "eHelp.xml"));
        }

        private string? FindFile(string pattern) =>
            Directory.Exists(Root) ? Directory.EnumerateFiles(Root, pattern).FirstOrDefault() : null;

        /// <summary>日本語の文言 (なければ英語) を返す。'&amp;' やアクセスキー表記 (&amp;G) は取り除く。</summary>
        public string T(string section, string key, string fallback) =>
            Strings.TryGetValue(section, out var s) && s.TryGetValue(key, out var v) && v.Length > 0
                ? Regex.Replace(v, @"\(&.\)|&", "")
                : fallback;

        private static Dictionary<string, IReadOnlyDictionary<string, string>> Merge(
            IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> baseStrings,
            IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> overlay)
        {
            var result = new Dictionary<string, IReadOnlyDictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
            foreach (var section in baseStrings.Keys.Union(overlay.Keys, StringComparer.OrdinalIgnoreCase))
            {
                var merged = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                if (baseStrings.TryGetValue(section, out var b)) foreach (var (k, v) in b) merged[k] = v;
                if (overlay.TryGetValue(section, out var o)) foreach (var (k, v) in o) merged[k] = v;
                result[section] = merged;
            }
            return result;
        }


        // ------------------------------------------------------------------ 元 HTML トピック

        public bool HasHtml(string id) => _htmlFiles.ContainsKey(id);

        /// <summary>パスで見つからないとき、ファイル名が一意に一致するトピック ID を返す (元データのリンク切れ対策)。</summary>
        public string? FindByFileName(string id)
        {
            var name = Path.GetFileName(id);
            var hits = _htmlFiles.Keys.Where(k => string.Equals(Path.GetFileName(k), name, StringComparison.OrdinalIgnoreCase)).ToList();
            return hits.Count == 1 ? hits[0] : null;
        }

        /// <summary>
        /// 元のトピック HTML を返す。CSS / ehlpdhtm.js / 画像の参照だけを wwwroot の配置に合わせて書き換える。
        /// トピック間の相対リンクと BSSCPopup / kadovTextPopup は元のまま動作させる。
        /// </summary>
        public string? ReadOriginalHtml(string id)
        {
            if (!_htmlFiles.TryGetValue(id, out var path)) return null;
            var html = File.ReadAllText(path, _sourceEncoding);
            html = AssetRef.Replace(html, m =>
            {
                var p = m.Groups["path"].Value;
                var url = p.StartsWith("image/", StringComparison.OrdinalIgnoreCase)
                    ? "/images/help/" + p["image/".Length..]
                    : p.EndsWith(".js", StringComparison.OrdinalIgnoreCase) ? "/js/" + p : "/css/" + p;
                return $"{m.Groups["attr"].Value}=\"{url}\"";
            });
            html = Regex.Replace(html, "charset=windows-1252", "charset=utf-8", RegexOptions.IgnoreCase);
            return html;
        }

        // ------------------------------------------------------------------ 資産一覧 (Resources ページ)

        public IReadOnlyList<HelpSourceFile> ProjectFiles()
        {
            if (!Directory.Exists(Root)) return [];
            var htmlCount = _htmlFiles.Count;
            var imageCount = Directory.Exists(Path.Combine(Root, "image"))
                ? Directory.GetFiles(Path.Combine(Root, "image")).Length : 0;
            var list = new List<HelpSourceFile>
            {
                new("HTMLFILE/", "HTMLFILE", DirSize(HtmlRoot), "フォルダー",
                    $"元のヘルプトピック ({htmlCount} ファイル)", "トピック (変換して使用)、Classic ビューアー (元のまま)"),
                new("image/", "image", DirSize(Path.Combine(Root, "image")), "フォルダー",
                    $"トピックで使う画像 ({imageCount} ファイル)", "トピック、Classic ビューアー、資料ページのギャラリー"),
            };
            foreach (var f in Directory.EnumerateFiles(Root).OrderBy(f => f))
            {
                var name = Path.GetFileName(f);
                var (kind, role, used) = Path.GetExtension(f).ToLowerInvariant() switch
                {
                    ".hhc" => ("目次", "ブックとページの構成", "目次サイドバー、ヘッダー、ホーム、Classic"),
                    ".hhk" => ("索引", "キーワードと該当トピック", "索引ページ、Classic の索引タブ"),
                    ".brs" => ("ブラウズシーケンス", "前へ / 次へ の順序", BrowseSequences.Count > 0 ? "Classic の前へ/次へ" : "Classic の前へ/次へ (空のため目次順を使用)"),
                    ".glo" => ("用語集", "用語と定義", Glossary.Count > 0 ? "Classic の用語集タブ" : "Classic の用語集タブ (空)"),
                    ".lng" when name.EndsWith(".ja.lng", StringComparison.OrdinalIgnoreCase) => ("UI 文言 (日本語)", "RoboHHRE.lng の日本語訳", "Classic ビューアーの表示、資料ページ"),
                    ".lng" => ("UI 文言", "WebHelp 画面の文言 (元の英語)", "日本語訳がない文言、資料ページ"),
                    ".xml" => ("eHelp オプション", "WebSearch ボタンの設定", "ヘッダーと Classic の Web 検索ボタン"),
                    ".gif" => ("画像", "mshtmlExp*.css の P.T3 / P.T4 の行頭記号", "wwwroot/css/t3.gif"),
                    _ => ("ファイル", "", ""),
                };
                list.Add(new(name, name, new FileInfo(f).Length, kind, role, used));
            }
            return list;
        }

        public IReadOnlyList<HelpImage> Images()
        {
            var dir = Path.Combine(Root, "image");
            if (!Directory.Exists(dir)) return [];
            var usage = new Dictionary<string, List<TopicInfo>>(StringComparer.OrdinalIgnoreCase);
            foreach (var (id, path) in _htmlFiles)
            {
                var text = File.ReadAllText(path, _sourceEncoding);
                foreach (Match m in Regex.Matches(text, "image/([A-Za-z0-9_.-]+\\.gif)", RegexOptions.IgnoreCase))
                {
                    var topic = _catalog.Find(id);
                    if (topic is null) continue;
                    var l = usage.TryGetValue(m.Groups[1].Value, out var x) ? x : usage[m.Groups[1].Value] = [];
                    if (!l.Contains(topic)) l.Add(topic);
                }
            }
            return Directory.EnumerateFiles(dir, "*.gif").OrderBy(f => f, StringComparer.OrdinalIgnoreCase)
                .Select(f =>
                {
                    var name = Path.GetFileName(f);
                    return new HelpImage(name, "/images/help/" + name, new FileInfo(f).Length,
                        usage.GetValueOrDefault(name) ?? []);
                }).ToList();
        }

        private static long DirSize(string dir) =>
            Directory.Exists(dir) ? Directory.EnumerateFiles(dir, "*", SearchOption.AllDirectories).Sum(f => new FileInfo(f).Length) : 0;

        // ------------------------------------------------------------------ パーサー

        private static Dictionary<string, IReadOnlyDictionary<string, string>> LoadLng(string? path)
        {
            var result = new Dictionary<string, IReadOnlyDictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
            if (path is null) return result;
            Dictionary<string, string>? current = null;
            foreach (var raw in File.ReadLines(path))
            {
                var line = raw.Trim();
                if (line.Length == 0 || line.StartsWith(';')) continue;
                if (line.StartsWith('[') && line.EndsWith(']'))
                {
                    current = new(StringComparer.OrdinalIgnoreCase);
                    result[line[1..^1]] = current;
                    continue;
                }
                var eq = line.IndexOf('=');
                if (eq <= 0 || current is null) continue;
                current[line[..eq].Trim()] = line[(eq + 1)..].Trim();
            }
            return result;
        }

        /// <summary>.glo: 「用語 [TAB] 定義」の行形式、または XML (&lt;term name=..&gt;定義&lt;/term&gt;) を読む。</summary>
        private static List<GlossaryTerm> LoadGlossary(string? path)
        {
            if (path is null) return [];
            var text = File.ReadAllText(path).Trim();
            if (text.Length == 0) return [];
            if (text.StartsWith('<'))
            {
                try
                {
                    return XDocument.Parse(text).Descendants()
                        .Where(e => e.Attribute("name") is not null || e.Element("term") is not null)
                        .Select(e => new GlossaryTerm(
                            (string?)e.Attribute("name") ?? (string?)e.Element("term") ?? "",
                            (string?)e.Element("definition") ?? e.Value))
                        .Where(t => t.Term.Length > 0).OrderBy(t => t.Term).ToList();
                }
                catch (System.Xml.XmlException) { return []; }
            }
            return text.Split('\n')
                .Select(l => l.Split('\t', 2))
                .Where(p => p.Length == 2)
                .Select(p => new GlossaryTerm(p[0].Trim(), p[1].Trim()))
                .OrderBy(t => t.Term).ToList();
        }

        /// <summary>.brs: XML 形式 (&lt;browsesequence name=..&gt;&lt;item url=..&gt;) を読む。空の場合は 0 件。</summary>
        private static List<BrowseSequence> LoadBrowseSequences(string? path)
        {
            if (path is null) return [];
            var text = File.ReadAllText(path).Trim();
            if (text.Length == 0 || !text.StartsWith('<')) return [];
            try
            {
                return XDocument.Parse(text).Descendants()
                    .Where(e => e.Name.LocalName.Equals("browsesequence", StringComparison.OrdinalIgnoreCase))
                    .Select(e => new BrowseSequence(
                        (string?)e.Attribute("name") ?? "Sequence",
                        e.Descendants().Select(i => (string?)i.Attribute("url") ?? (string?)i.Attribute("src"))
                            .Where(u => u is not null)
                            .Select(u => Path.ChangeExtension(u!.Replace('\\', '/'), null)
                                .Replace("HTMLFILE/", "", StringComparison.OrdinalIgnoreCase))
                            .ToList()))
                    .ToList();
            }
            catch (System.Xml.XmlException) { return []; }
        }

        /// <summary>eHelp.xml の FIRST-PAGE (NOT-SET のときは null)。</summary>
        private static string? LoadFirstPage(string path)
        {
            if (!File.Exists(path)) return null;
            var m = Regex.Match(File.ReadAllText(path), "FIRST-PAGE=\"([^\"]*)\"");
            return m.Success && m.Groups[1].Value != "NOT-SET" ? m.Groups[1].Value : null;
        }
    }
}
