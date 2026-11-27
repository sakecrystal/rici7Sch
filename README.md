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

ホーム (`/`) とヘッダーから、以下のすべてのページと全 140 トピックに移動できます
（ヘッダーの **Topics** メニュー、ホームの Contents / All topics A–Z）。

| URL | ページ | 内容 |
| --- | --- | --- |
| `/` | `Pages/Index` | ホーム。全ページ・全セクション・全トピックへの入口 |
| `/Topics/{ID}` | `Pages/Topics/**` | 移植したヘルプトピック 140 件 |
| `/Keywords` | `Pages/Keywords` | 索引（.hhk）。頭文字ジャンプ、入力による絞り込み |
| `/Search?q=` | `Pages/Search` | 全文検索。`"..."` でフレーズ検索 |
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
| `*.hhc`（目次） | サイドバー、ヘッダーの Topics メニュー、ホーム、Classic の目次タブ |
| `*.hhk`（索引） | Index、Classic の索引タブ |
| `*.brs`（ブラウズシーケンス） | Classic の前へ/次へ（元データが空のため目次順を使用） |
| `*.glo`（用語集） | Classic の用語集タブ（元データが空） |
| `RoboHHRE.lng`（UI 文言） | Classic のボタン・ラベル、Resources |
| `eHelp.xml`（WebSearch 設定） | ヘッダー / Classic の WebSearch ボタン（元の設定どおり無効）、Resources |
| `wwwroot/js/ehlpdhtm.js` | Classic（元のドロップダウン・ポップアップ効果） |

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
    Shared/                      _Layout・_TopicLayout・_TocTree
    Topics/                      移植したトピック (自動生成) と TopicPageModel.cs
  Data/                          目次・索引・検索用データ (自動生成)
  HelpSource/                    移植元の RoboHelp 資産
  tools/convert_help.py          変換ツール
  wwwroot/
    css/   site.css, mshtmlExpNonbrand.css, mshtmlExp2nd.css, t3.gif
    js/    site.js, classic.js, ehlpdhtm.js
    images/help/                 画像 (自動コピー)
    lib/bootstrap/
```

## トピックを再生成するには

`HelpSource/` の元資産を変更した場合は、変換ツールで `Pages/Topics/`・`Data/`・`wwwroot/images/help/`・`wwwroot/css/t3.gif` を作り直します。
VS Code のタスク **convert-help** からも実行できます。

```bash
pip install beautifulsoup4   # 初回のみ
python3 rici7Sch/tools/convert_help.py
```

`Pages/Topics/` 内の生成ファイルは上書きされるため、直接編集しないでください。

## 更新履歴

- 2026-11-27: Razor Pages プロジェクトを作成。Index / Error / Privacy を作成し、RoboHelp ヘルプ 140 トピック・目次・索引・検索を移植。VS Code の「実行とデバッグ」設定を追加。
- 2026-11-27: プロジェクトを `rici7Sch/` に移動。`rici7Sch.slnx` にプロジェクトを登録し、VS Code 設定のパスを更新。元資産を `HelpSource/` に整理。`mshtmlExpNonbrand.css` / `mshtmlExp2nd.css` / `site.css` をレイヤーで重ねて UI に使用。Classic ビューアー（`ehlpdhtm.js`・.lng・.brs・.glo・eHelp.xml を使用）と Resources ページを追加。ホームとヘッダーから全ページ・全トピックへ移動できるように変更。
- 2026-11-27: VS Code のエクスプローラーで `.cshtml.cs` を `.cshtml` の下に重ねて表示する設定を追加。
