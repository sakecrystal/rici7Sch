using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using rici7Sch.Services;

namespace rici7Sch.Pages;

/// <summary>索引 (.hhk) ページ。キーワードを頭文字ごとに表示し、入力で絞り込む。</summary>
public class KeywordsModel(HelpCatalog catalog) : PageModel
{
    [BindProperty(SupportsGet = true, Name = "k")]
    public string? Filter { get; set; }

    public IReadOnlyList<IGrouping<string, KeywordEntry>> Groups { get; private set; } = [];
    public int Total { get; private set; }

    public void OnGet()
    {
        IEnumerable<KeywordEntry> entries = catalog.Keywords;
        if (!string.IsNullOrWhiteSpace(Filter))
            entries = entries.Where(e => e.Keyword.Contains(Filter.Trim(), StringComparison.OrdinalIgnoreCase));
        var list = entries.ToList();
        Total = list.Count;
        Groups = list.GroupBy(e => GroupKey(e.Keyword)).ToList();
    }

    public static string GroupKey(string keyword)
    {
        var c = keyword.TrimStart('[', '(', '"').FirstOrDefault();
        return char.IsLetter(c) ? char.ToUpperInvariant(c).ToString() : "#";
    }

    public static string Slug(string key) => key == "#" ? "sym" : key;
}
