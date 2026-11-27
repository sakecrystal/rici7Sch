using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using rici7Sch.Services;

namespace rici7Sch.Pages;

/// <summary>全文検索ページ (旧 WebHelp の Search タブ相当)。</summary>
public class SearchModel(HelpCatalog catalog) : PageModel
{
    [BindProperty(SupportsGet = true, Name = "q")]
    public string? Query { get; set; }

    public IReadOnlyList<SearchHit> Results { get; private set; } = [];
    public IReadOnlyList<string> Terms { get; private set; } = [];

    public void OnGet()
    {
        Terms = HelpCatalog.Tokenize(Query);
        Results = catalog.Search(Query);
    }
}
