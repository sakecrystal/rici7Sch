using Microsoft.AspNetCore.Mvc.RazorPages;
using rici7Sch.Services;

namespace rici7Sch.Pages;

/// <summary>ホームページ。すべてのページ・すべてのトピックへの入口。</summary>
public class IndexModel(HelpCatalog catalog) : PageModel
{
    public sealed record PageLink(string Title, string Url, string Description);

    public IReadOnlyList<TocNode> Sections { get; private set; } = [];
    public IReadOnlyList<IGrouping<char, TopicInfo>> AllTopics { get; private set; } = [];
    public IReadOnlyList<TopicInfo> OtherTopics { get; private set; } = [];
    public int TopicCount { get; private set; }
    public int KeywordCount { get; private set; }

    /// <summary>サイト内のページ一覧 (ヘッダーと同じ並び)。</summary>
    public IReadOnlyList<PageLink> Pages { get; } =
    [
        new("Contents", "/Topics/IDH_CONTENTS", "Start with “Using Help” and browse the table of contents"),
        new("Index", "/Keywords", "Browse alphabetically by keyword"),
        new("Search", "/Search", "Find topics containing specific words"),
        new("Classic Viewer", "/Classic", "Original pages, styles and effects in a WebHelp-style viewer"),
        new("Resources", "/Resources", "Images, project files, UI strings, styles and scripts"),
        new("Privacy", "/Privacy", "What this site stores and sends"),
        new("Error", "/Error", "The page shown when something goes wrong"),
    ];

    public void OnGet()
    {
        Sections = catalog.Toc;
        TopicCount = catalog.Topics.Count();
        KeywordCount = catalog.Keywords.Count;
        AllTopics = catalog.Topics
            .OrderBy(t => t.Title, StringComparer.OrdinalIgnoreCase)
            .GroupBy(t => char.IsLetter(t.Title.FirstOrDefault()) ? char.ToUpperInvariant(t.Title[0]) : '#')
            .ToList();
        OtherTopics = catalog.TopicsNotInToc.ToList();
    }

    /// <summary>本 (フォルダ) の中で最初に開けるトピックの URL。</summary>
    public static string? FirstUrl(TocNode node) =>
        node.Url ?? node.Children.Select(FirstUrl).FirstOrDefault(u => u is not null);

    public static int CountTopics(TocNode node) =>
        (node.Topic is null ? 0 : 1) + node.Children.Sum(CountTopics);
}
