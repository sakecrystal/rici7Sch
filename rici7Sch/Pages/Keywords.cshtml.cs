using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using rici7Sch.Services;

namespace rici7Sch.Pages;

/// <summary>索引 (.hhk) ページ。キーワードを元の英語の頭文字ごとに表示し、日本語・英語のどちらでも絞り込める。</summary>
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
            entries = entries.Where(e => e.Keyword.Contains(Filter.Trim(), StringComparison.OrdinalIgnoreCase) ||
                                         e.KeywordEn.Contains(Filter.Trim(), StringComparison.OrdinalIgnoreCase));
        var list = entries.ToList();
        Total = list.Count;
        // 元の索引 (英語) の頭文字でまとめる
        Groups = list.GroupBy(e => GroupKey(string.IsNullOrEmpty(e.KeywordEn) ? e.Keyword : e.KeywordEn)).ToList();
    }

    public static string GroupKey(string keyword)
    {
        var c = keyword.TrimStart('[', '(', '"').FirstOrDefault();
        return char.IsLetter(c) ? char.ToUpperInvariant(c).ToString() : "#";
    }

    public static string Slug(string key) => key == "#" ? "sym" : key;
}
