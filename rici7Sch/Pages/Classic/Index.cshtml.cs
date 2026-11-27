using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using rici7Sch.Services;

namespace rici7Sch.Pages.Classic;

/// <summary>
/// 旧 WebHelp と同じ操作感のビューアー。
/// 目次 (.hhc)・索引 (.hhk)・検索・用語集 (.glo) のタブ、ブラウズシーケンス (.brs)、
/// RoboHHRE.lng の文言、eHelp.xml のオプションを使い、右側に元のトピックを表示する。
/// </summary>
public class IndexModel(HelpCatalog catalog, HelpSourceService source) : PageModel
{
    public HelpCatalog Catalog => catalog;
    public HelpSourceService Source => source;

    [BindProperty(SupportsGet = true)]
    public string? Topic { get; set; }

    public string StartTopic { get; private set; } = "IDH_CONTENTS";

    /// <summary>前へ / 次へ に使う順序。.brs が空なら目次順。</summary>
    public IReadOnlyList<string> Sequence { get; private set; } = [];
    public bool SequenceFromBrs { get; private set; }

    public IActionResult OnGet()
    {
        var first = source.FirstPage is not null ? Path.ChangeExtension(source.FirstPage, null) : null;
        StartTopic = new[] { Topic, first, "IDH_CONTENTS" }
            .FirstOrDefault(t => t is not null && source.HasHtml(t)) ?? catalog.Sequence.FirstOrDefault() ?? "";
        var brs = source.BrowseSequences.FirstOrDefault(b => b.Topics.Count > 0);
        SequenceFromBrs = brs is not null;
        Sequence = brs?.Topics ?? catalog.Sequence;
        return Page();
    }

    /// <summary>Search タブ用の全文検索 (JSON)。</summary>
    public IActionResult OnGetSearch(string? q) =>
        new JsonResult(catalog.Search(q, 50).Select(h => new { id = h.Topic.Id, title = h.Topic.Title }));

    public static string FrameUrl(string id) => "/Classic/HTMLFILE/" + id + ".htm";
}
