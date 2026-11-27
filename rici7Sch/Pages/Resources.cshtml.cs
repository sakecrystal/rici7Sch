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
            ("css/site.css", "Site layout, header, sidebar and pages (loaded last)"),
            ("css/mshtmlExpNonbrand.css", "Original topic style: headings, T1/T2/T3, Note, Term-* (all topics, Classic)"),
            ("css/mshtmlExp2nd.css", "Original popup-topic style (topics that were opened as popups)"),
            ("css/t3.gif", "Bullet image referenced by mshtmlExp*.css"),
            ("js/site.js", "Dropdowns, popups, index filter, theme, print"),
            ("js/ehlpdhtm.js", "Original RoboHelp DHTML effects (Classic viewer)"),
            ("js/classic.js", "Classic viewer navigation (tabs, sync, previous / next)"),
            ("favicon.ico", "Site icon"),
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
