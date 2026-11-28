using Microsoft.AspNetCore.Mvc.RazorPages;
using rici7Sch.Services;

namespace rici7Sch.Pages;

/// <summary>
/// ヘルプの資産ページ: 画像ギャラリー、プロジェクトファイル (.hhc/.hhk/.brs/.glo/.lng/eHelp.xml)、
/// UI 文言 (RoboHHRE.lng)、スタイルシート・スクリプトの一覧。
/// </summary>
public class ResourcesModel(HelpSourceService source, HelpCatalog catalog, IWebHostEnvironment env) : PageModel
{
    public HelpSourceService Source => source;
    public HelpCatalog Catalog => catalog;
    public IReadOnlyList<HelpSourceFile> Files { get; private set; } = [];
    public IReadOnlyList<HelpImage> Images { get; private set; } = [];
    public IReadOnlyList<(string Url, string Name, long Size, string Role)> Assets { get; private set; } = [];

    public void OnGet()
    {
        Files = source.ProjectFiles();
        Images = source.Images();
        (string Path, string Role)[] assets =
        [
            ("css/site.css", "サイトのレイアウト・ヘッダー・サイドバー・各ページ (最後に適用)"),
            ("css/mshtmlExpNonbrand.css", "元のトピック用スタイル: 見出し・T1/T2/T3・Note・Term-* (全トピック、Classic)"),
            ("css/mshtmlExp2nd.css", "元のポップアップ用スタイル (ポップアップで開いていたトピック)"),
            ("css/t3.gif", "mshtmlExp*.css が参照する行頭記号の画像"),
            ("js/site.js", "ドロップダウン・ポップアップ・索引の絞り込み・テーマ・印刷"),
            ("js/ehlpdhtm.js", "元の RoboHelp DHTML スクリプト (トピックのドロップダウンの開閉、Classic 表示のポップアップ・ドロップダウン)"),
            ("js/classic.js", "Classic 表示のナビゲーション (タブ・目次の同期・前へ / 次へ)"),
            ("favicon.ico", "サイトのアイコン"),
        ];
        Assets = assets.Select(a =>
        {
            var file = env.WebRootFileProvider.GetFileInfo(a.Path);
            return ("/" + a.Path, System.IO.Path.GetFileName(a.Path), file.Exists ? file.Length : 0L, a.Role);
        }).ToList();
    }

    public static string FormatSize(long bytes) =>
        bytes >= 1024 * 1024 ? $"{bytes / 1024.0 / 1024.0:0.0} MB" : bytes >= 1024 ? $"{bytes / 1024.0:0.0} KB" : $"{bytes} B";
}
