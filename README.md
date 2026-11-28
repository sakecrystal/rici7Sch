# rici7Sch

RoboHelp 形式のプリンタードライバーヘルプ（HTML / XML / 目次 / 索引）を、
**ASP.NET Core Razor Pages (.NET 10)** の Web アプリに移植したプロジェクトです。

- 通常のページ (`/Topics/...`) は、元の HTML を変換したうえで共通レイアウトに組み込み、元の機能を作り直しています。
- **Classic ビューアー** (`/Classic`) では、元の HTML・CSS・`ehlpdhtm.js` をそのまま使い、旧 WebHelp と同じ操作で閲覧できます。

## 実行方法

### Visual Studio / VS Code でソリューションを開く

ルートの **`rici7Sch.slnx`** を開きます（プロジェクト `rici7Sch/rici7Sch.csproj` が登録済み）。

### VS Code の「実行とデバッグ」から

1. 推奨拡張機能 **C# (ms-dotnettools.csharp)** をインストールします（`.vscode/extensions.json` に登録済み）。
2. 「実行とデバッグ」(Ctrl+Shift+D) で **「ASP.NET Core 起動 (rici7Sch)」** を選んで ▶ (F5)。
   - `rici7Sch.slnx` をビルド → 起動 → ブラウザで http://localhost:5276 が開きます。
3. 停止はデバッグツールバーの ■ (Shift+F5)。

エクスプローラーでは `.cshtml` の下に `.cshtml.cs` が重ねて表示されます（`.vscode/settings.json` のファイルネスト設定。Visual Studio では標準で重なります）。

### コマンドラインから

```bash
dotnet build rici7Sch.slnx
cd rici7Sch && dotnet run      # http://localhost:5276
```

## ページ一覧

画面の文言とトピックの本文・タイトル・目次・索引はすべて日本語です。ヘッダー右端のボタンでライト / ダーク表示を切り替えられます。各トピックには元の英語のタイトルを表示し、「原文 (英語)」ボタンから元のページを Classic 表示で開けます。検索は日本語でも英語でもヒットします。

ホーム (`/`) とヘッダーから、以下のすべてのページと全 140 トピックに移動できます
（ヘッダーの **トピック** メニュー、ホームの「目次」「全トピック」）。

| URL | ページ | 内容 |
| --- | --- | --- |
| `/` | `Pages/Index` | ホーム。全ページ・全セクション・全トピックへの入口 |
| `/Topics/{ID}` | `Pages/Topics/**` | 移植したヘルプトピック 140 件（このページの内容・関連トピック・索引キーワード付き） |
| `/Topics/{ID}?handler=Fragment` | `Pages/Topics/**` | ポップアップ用に本文だけを返す（モーダルが読み込む） |
| `/Keywords` | `Pages/Keywords` | 索引（.hhk）。元の英語キーワードの頭文字でまとめ、日本語キーワードに英語を併記。日本語・英語のどちらでも絞り込み（`?k=` で指定も可） |
| `/Search?q=` | `Pages/Search` | 全文検索。日本語・英語（原文）のどちらでもヒット。すべての語を含むトピックを表示し、`"..."` でフレーズ検索 |
| `/Classic` | `Pages/Classic/Index` | 旧 WebHelp 形式のビューアー（目次・索引・検索・用語集タブ、前へ/次へ、目次の同期、ナビゲーションの表示/非表示、印刷） |
| `/Classic/HTMLFILE/*.htm` | `Pages/Classic/Frame` | Classic ビューアーの右側に表示する元のトピック |
| `/Resources` | `Pages/Resources` | 画像ギャラリー（使用トピック付き）、プロジェクトファイル、CSS/JS、UI 文言（.lng）、オプション |
| `/Privacy` | `Pages/Privacy` | プライバシー |
| `/Error` | `Pages/Error` | エラー画面（404 などもここで表示） |
| `/HTMLFILE/*.htm` | `Program.cs` | 旧 URL から新しいトピックへのリダイレクト |

## スタイルシート

`wwwroot/css` の 3 ファイルを、CSS のカスケードレイヤーで重ねて使っています（`site.css` の先頭で宣言）。

| 順序 | ファイル | 役割 |
| --- | --- | --- |
| 1 (最下層) | `lib/bootstrap` | 部品・グリッド |
| 2 | `mshtmlExpNonbrand.css` | 元のヘルプのスタイル（書体、見出し、`P.T1`〜`T4`、`P.Note`、`span.Term-*`、リンク）。全ページの本文に適用 |
| 2 | `mshtmlExp2nd.css` | 元のポップアップ用スタイル。元々このファイルを使っていた 6 トピックで追加読み込み |
| 3 (最上層) | `site.css` | レイアウト・ヘッダー・サイドバー・ダークモード。UI 部品に効く元スタイルだけを打ち消す |

変換後のトピックは元の class 名（`T2`、`Note`、`Term-Soft` など）を残しているため、元の CSS がそのまま効きます。
`t3.gif` は元の CSS が `url(t3.gif)` で参照するため `wwwroot/css/` に置いています。

## 元のヘルプ資産とその使い道

| ファイル (`rici7Sch/HelpSource/`) | 使っているページ |
| --- | --- |
| `HTMLFILE/*.htm` | Topics（変換して使用）、Classic（元のまま表示） |
| `image/*.gif` | Topics、Classic、Resources（ギャラリー） |
| `*.hhc`（目次） | サイドバー、ヘッダーの「トピック」メニュー、ホーム、パンくずリスト、前へ/次へ、Classic の目次タブ |
| `*.hhk`（索引） | 索引ページ、各トピックの「索引キーワード」、Classic の索引タブ |
| `*.brs`（ブラウズシーケンス） | Classic の前へ/次へ（元データが空のため目次順を使用） |
| `*.glo`（用語集） | Classic の用語集タブ（元データが空） |
| `RoboHHRE.lng`（UI 文言・英語） | 日本語訳がない文言の予備、Resources |
| `RoboHHRE.ja.lng`（UI 文言・日本語） | Classic のボタン・ラベル、Resources（英語と並べて表示） |
| `ja/*.json`（本文の日本語訳） | 変換ツールが Topics・目次・索引の日本語化に使用 |
| `eHelp.xml`（WebSearch 設定） | ヘッダー / Classic の WebSearch ボタン（元の設定どおり無効）、Resources |
| `wwwroot/js/ehlpdhtm.js` | ドロップダウンのあるトピックでの開閉（`kadovTextPopup`）、Classic（元のドロップダウン・ポップアップ効果） |

## フォルダ構成

```
rici7Sch.slnx                    ソリューション
.vscode/                         実行とデバッグ・タスク設定
rici7Sch/                        プロジェクト
  rici7Sch.csproj
  Program.cs                     ルーティング・エラーページ・旧URL互換
  Services/
    HelpCatalog.cs               目次・索引・検索・前後トピック (Data/*.json)
    HelpSourceService.cs         元資産の読み込み (.lng/.brs/.glo/eHelp.xml/画像/元HTML)
  Pages/
    Index, Error, Privacy        (.cshtml + .cshtml.cs)
    Keywords, Search, Resources  (.cshtml + .cshtml.cs)
    Classic/Index, Classic/Frame (.cshtml + .cshtml.cs)
    Shared/                      _Layout・_TopicLayout・_TopicFragment・_TocTree
    Topics/                      移植したトピック (自動生成) と TopicPageModel.cs・_ViewStart
  Data/                          目次・索引・検索用データ (自動生成)
  HelpSource/                    移植元の RoboHelp 資産
  tools/convert_help.py          変換ツール
  tools/apply_translation.py     翻訳作業用 (番号付きの訳文を対訳辞書に書き出す)
  wwwroot/
    css/   site.css, mshtmlExpNonbrand.css, mshtmlExp2nd.css, t3.gif
    js/    site.js, classic.js, ehlpdhtm.js
    images/help/                 画像 (自動コピー)
    lib/bootstrap/
scripts/claude-sessions.sh       古い Claude セッション・残ったアプリの停止
```

## トピックページの仕組み (.cshtml と .cshtml.cs)

`Pages/Topics/` の各トピックは、変換ツールが生成する 2 つのファイルでできています。

| ファイル | 内容 |
| --- | --- |
| `{ID}.cshtml` | 本文（日本語訳を適用したマークアップ） |
| `{ID}.cshtml.cs` | そのトピックの情報を持つ PageModel（`TopicPageModel` を継承） |

生成される `.cshtml.cs` の例:

```csharp
public class IDH_DDUPLEXModel : TopicPageModel
{
    public override string TopicId => "IDH_DDUPLEX";
    public override string SourceFile => "HTMLFILE/IDH_DDUPLEX.htm";
    public override string OriginalTitle => "2 Sided/Layout/Booklet";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;
    public override IReadOnlyList<TopicSection> Sections { get; } = [ new("layout", "レイアウト：", 2), ... ];
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_HOW_LAYOUT", ...];
}
```

基底クラス `Pages/Topics/TopicPageModel.cs` がこれらの情報を使って、各トピックページに次の機能を付けます。

| 情報 | 使い道 |
| --- | --- |
| `Sections`（ページ内の見出し） | 「このページの内容」（見出しが 3 つ以上のとき。項目が多いときは折りたたみ） |
| `LinkedTopics` / `PopupTopics` | 「関連トピック」（ポップアップ先はモーダルで開く） |
| 全トピックの参照先（`Data/topics.json` の `links`） | 「このページを参照しているトピック」 |
| 索引（`.hhk`） | 「索引キーワード」（クリックすると索引ページで絞り込み） |
| `OriginalTitle` / `SourceFile` | 原文のタイトル表示、「原文 (英語)」ボタン |
| `Stylesheet` | `mshtmlExp2nd.css` を重ねるかどうか |
| `DropdownCount` | ドロップダウンがあるページだけ `ehlpdhtm.js` を読み込み、「すべて展開」ボタンを表示 |
| `OnGetFragment` ハンドラー | `?handler=Fragment` で本文だけを返す（`_TopicFragment` レイアウト）。ポップアップのモーダルが使う |

## トピックを再生成するには

`HelpSource/` の元資産を変更した場合は、変換ツールで `Pages/Topics/`・`Data/`・`wwwroot/images/help/`・`wwwroot/css/t3.gif` を作り直します。
VS Code のタスク **convert-help** からも実行できます。

```bash
pip install beautifulsoup4   # 初回のみ
python3 rici7Sch/tools/convert_help.py
```

`Pages/Topics/` 内の生成ファイルは上書きされるため、直接編集しないでください。

### 日本語訳について

- 訳文は `HelpSource/ja/*.json` に「英語 → 日本語」の対訳として保存しています（段落・リスト項目などの単位。リンクや画像などのタグはそのまま残します）。
- 画面の項目名（よく使う設定、詳細設定など）の訳語は `HelpSource/ja/00-terms.json` で統一しています。項目名だけの見出しやトピック名だけのリンクは、これらの訳を自動で当てはめます。
- 変換ツールを実行すると、訳がない文が `tools/untranslated.json` に書き出されます（現在は 0 件）。訳を直したいときは `HelpSource/ja/*.json` を編集して変換ツールを再実行してください。
- 画面の項目名は一般的なプリンタードライバーの用語で訳しているため、実際の日本語版ドライバーの表示と異なる場合があります。

### ehlpdhtm.js の使い方

- ドロップダウンのあるトピックページでは `site.js` より前に `ehlpdhtm.js` を読み込み、ドロップダウン（「▼」付きの語句をクリックすると開く説明）の開閉を元のヘルプと同じ `kadovTextPopup` で行います。`ehlpdhtm.js` が読み込めない・エラーになる場合は `site.js` が代わりに開閉します（`<html data-dropdown-engine="ehlpdhtm">` で、どちらで動いているか確認できます）。
- ファイルポップアップ（`BSSCPopup`）は、通常のページではモーダルで表示します。`ehlpdhtm.js` のポップアップは単位なしの数値で位置やサイズを指定しており、互換モード（元の HTML）でしか正しく動かないため、Classic 表示でのみ使っています。

## 古い Claude セッションの停止

VS Code 拡張機能を再起動したあとなどに古い Claude Code のセッションが残り、「この会話は別の場所で開いています」と表示されることがあります。次の方法でいつでも止められます。

- コマンドパレット (Ctrl+Shift+P) → **Tasks: Run Task** → 次のいずれかを選ぶ
  - **Claude: セッション一覧** … 動いているセッションを表示
  - **Claude: 古いセッションを停止** … 最新以外のセッションを停止（確認してから停止）
  - **アプリ: 残っている rici7Sch を停止** … 「Failed to bind to address http://127.0.0.1:5276」が出たときに、残っているアプリを停止
- ターミナルから:

```bash
scripts/claude-sessions.sh list          # 一覧
scripts/claude-sessions.sh stop-old      # 最新以外を停止 (-y で確認なし)
scripts/claude-sessions.sh stop <PID>    # 指定したセッションを停止
scripts/claude-sessions.sh stop-app      # 残っている rici7Sch アプリを停止
```

開始時刻が最も新しいセッション（いま使っているもの）と、スクリプトを実行しているセッション自身は止めません。停止はまず通常の終了要求 (SIGTERM) を送り、5 秒たっても終わらない場合だけ強制終了します。

## 既知の制限と注意点

- **ブラウザでの表示確認**: 開発環境にブラウザがないため、動作確認はサーバーの応答（全ページ・全リンク・本文だけの応答）と、`ehlpdhtm.js` を簡易的な疑似 DOM で動かす確認までです。見た目やモーダル・ドロップダウンの操作は、実際のブラウザで確認してください。
- **ファイルポップアップ（`BSSCPopup`）**: 通常のページではモーダルで表示します。`ehlpdhtm.js` のポップアップは互換モードのページでしか正しく配置されないため Classic 表示でのみ使います。Chrome / Edge では別ウィンドウで開く動作になり、元のスクリプトにある綴りの誤り（`widnow`）でエラーが出ることがあります。
- **画面の項目名の訳語**: 一般的なプリンタードライバーの用語で訳しているため、実際の日本語版ドライバーの表示と異なる場合があります（`HelpSource/ja/00-terms.json` で修正できます）。
- **元データのリンク切れ**: `idh_dadvanced_auth` から `IDH_HOW_AUTHENTICATION` へのリンクは元データでパスが誤っているため、同名のファイル（`PCL_CS/IDH_HOW_AUTHENTICATION`）につなぎ直しています。
- **ポート 5276 が使用中**: 前のデバッグセッションが残っていると起動に失敗します。デバッグの再起動は Ctrl+Shift+F5 を使うか、タスク「アプリ: 残っている rici7Sch を停止」で止めてください。
- **生成ファイル**: `Pages/Topics/`（`TopicPageModel.cs`・`_ViewStart.cshtml` 以外）と `Data/` は変換ツールが上書きします。直接編集せず、変換ツールか対訳辞書を変更してください。

## 更新履歴

- 2026-11-27: Razor Pages プロジェクトを作成。Index / Error / Privacy を作成し、RoboHelp ヘルプ 140 トピック・目次・索引・検索を移植。VS Code の「実行とデバッグ」設定を追加。
- 2026-11-27: プロジェクトを `rici7Sch/` に移動。`rici7Sch.slnx` にプロジェクトを登録し、VS Code 設定のパスを更新。元資産を `HelpSource/` に整理。`mshtmlExpNonbrand.css` / `mshtmlExp2nd.css` / `site.css` をレイヤーで重ねて UI に使用。Classic ビューアー（`ehlpdhtm.js`・.lng・.brs・.glo・eHelp.xml を使用）と Resources ページを追加。ホームとヘッダーから全ページ・全トピックへ移動できるように変更。
- 2026-11-27: VS Code のエクスプローラーで `.cshtml.cs` を `.cshtml` の下に重ねて表示する設定を追加。
- 2026-11-27: UI を日本語化（ヘッダー、各ページ、ボタン、JS のメッセージ）。Classic 用に `RoboHHRE.ja.lng` を追加し、英語版に重ねて読み込むように変更。日本語フォントの代替指定と、日本語を文字参照にせず出力する設定を追加。
- 2026-11-27: 移植したトピック 140 件・目次・索引を日本語化。対訳辞書 `HelpSource/ja/*.json` と、変換ツールで訳を適用する仕組みを追加。トピックに元の英語タイトルと原文へのリンクを表示。索引は英語の頭文字でまとめ、日本語・英語の両方で絞り込み可能に。Classic 表示の目次・索引は元の英語名で表示。
- 2026-11-27: `ehlpdhtm.js` を通常のトピックページでも読み込み、ドロップダウンの開閉に `kadovTextPopup` を使用（使えない場合は自前で開閉）。
- 2026-11-28: 古い Claude セッションと残ったアプリを停止するスクリプト `scripts/claude-sessions.sh` と VS Code タスクを追加。
- 2026-11-28: 生成するトピックの `.cshtml.cs` に、元ファイル名・英語タイトル・スタイルシート・ページ内の見出し・参照先・ポップアップ先・ドロップダウン数を持たせるように変更。これを使って「このページの内容」「関連トピック」「このページを参照しているトピック」「索引キーワード」を表示。ポップアップは `OnGetFragment`（本文だけの応答）を読み込むように変更し、`ehlpdhtm.js` はドロップダウンのあるページだけで読み込むように変更。README を現在の構成に合わせて更新。
- 2026-11-28: README の古い記述（メニュー名、索引・検索の説明、元資産の使い道）を修正し、「既知の制限と注意点」を追加。
