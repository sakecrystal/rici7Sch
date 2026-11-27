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
        new("目次", "/Topics/IDH_CONTENTS", "「Using Help」(ヘルプの使い方) から目次をたどる"),
        new("索引", "/Keywords", "キーワードをアルファベット順に探す"),
        new("検索", "/Search", "語句を含むトピックを全文検索する"),
        new("Classic 表示", "/Classic", "元のページ・スタイル・効果を WebHelp 形式で表示する"),
        new("資料", "/Resources", "画像・プロジェクトファイル・UI 文言・CSS / JS の一覧"),
        new("プライバシー", "/Privacy", "このサイトが保存・送信する情報"),
        new("エラー", "/Error", "問題が発生したときに表示されるページ"),
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
