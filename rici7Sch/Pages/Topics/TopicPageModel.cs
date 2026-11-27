using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using rici7Sch.Services;

namespace rici7Sch.Pages.Topics;

/// <summary>
/// 移植した各ヘルプトピックページの基底クラス。
/// 目次上の位置 (パンくず) と前後のトピック (ブラウズシーケンス) を解決する。
/// </summary>
public abstract class TopicPageModel : PageModel
{
    /// <summary>HTMLFILE からの相対パス (拡張子なし)。例: "RPCS/IDH_HOW_CREEP"</summary>
    public abstract string TopicId { get; }

    public TopicNavigation Navigation { get; private set; } = default!;

    public string Title => Navigation.Topic.Title;

    public IActionResult OnGet([FromServices] HelpCatalog catalog)
    {
        var nav = catalog.GetNavigation(TopicId);
        if (nav is null) return NotFound();
        Navigation = nav;
        ViewData["Title"] = nav.Topic.Title;
        ViewData["TopicId"] = TopicId;
        ViewData["TopicNavigation"] = nav;
        ViewData["Stylesheet"] = nav.Topic.Stylesheet;
        return Page();
    }
}
