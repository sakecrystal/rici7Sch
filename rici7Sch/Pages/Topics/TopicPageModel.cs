using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using rici7Sch.Services;

namespace rici7Sch.Pages.Topics;

/// <summary>ページ内の見出し (「このページの内容」に表示)。Level 1 = 区切り (T1)、2 = 項目 (T2)。</summary>
public sealed record TopicSection(string Id, string Title, int Level);

/// <summary>
/// 移植した各ヘルプトピックページの基底クラス。
/// 各トピックの .cshtml.cs (tools/convert_help.py が生成) がトピックごとの情報をオーバーライドし、
/// この基底クラスがそれを使って次の機能を提供する。
/// <list type="bullet">
/// <item>目次上の位置 (パンくず) と前後のトピック (ブラウズシーケンス)</item>
/// <item>このページの内容 (ページ内の見出しへのリンク)</item>
/// <item>関連トピック (本文から参照しているトピック) と、このページを参照しているトピック</item>
/// <item>このトピックに対応する索引キーワード</item>
/// <item>ポップアップ表示用の本文だけの応答 (?handler=Fragment)</item>
/// </list>
/// </summary>
public abstract class TopicPageModel : PageModel
{
    // ---------------------------------------------------------------- 生成された .cshtml.cs が設定する情報

    /// <summary>HTMLFILE からの相対パス (拡張子なし)。例: "RPCS/IDH_HOW_CREEP"</summary>
    public abstract string TopicId { get; }

    /// <summary>移植元のファイル (HelpSource からの相対パス)。</summary>
    public virtual string SourceFile => $"HTMLFILE/{TopicId}.htm";

    /// <summary>元の英語のタイトル。</summary>
    public virtual string OriginalTitle => "";

    /// <summary>元のトピックが使っていたスタイルシート (mshtmlExpNonbrand / mshtmlExp2nd)。</summary>
    public virtual string Stylesheet => "mshtmlExpNonbrand";

    /// <summary>ドロップダウン (kadovTextPopup) の数。0 のページでは ehlpdhtm.js を読み込まない。</summary>
    public virtual int DropdownCount => 0;

    /// <summary>ページ内の見出し。</summary>
    public virtual IReadOnlyList<TopicSection> Sections { get; } = [];

    /// <summary>本文から参照しているトピックの ID。</summary>
    public virtual IReadOnlyList<string> LinkedTopics { get; } = [];

    /// <summary>ポップアップで開くトピックの ID。</summary>
    public virtual IReadOnlyList<string> PopupTopics { get; } = [];

    // ---------------------------------------------------------------- 要求ごとに解決する情報

    public TopicNavigation Navigation { get; private set; } = default!;
    public string Title => Navigation.Topic.Title;

    /// <summary>関連トピック (本文・ポップアップの参照先)。</summary>
    public IReadOnlyList<TopicInfo> RelatedTopics { get; private set; } = [];

    /// <summary>このページを参照しているトピック。</summary>
    public IReadOnlyList<TopicInfo> ReferencedBy { get; private set; } = [];

    /// <summary>このトピックに対応する索引キーワード。</summary>
    public IReadOnlyList<KeywordEntry> Keywords { get; private set; } = [];

    /// <summary>「このページの内容」を表示するか (見出しが 3 つ以上あるとき)。</summary>
    public bool ShowSections => Sections.Count >= 3;

    public IActionResult OnGet([FromServices] HelpCatalog catalog) => Load(catalog, fragment: false);

    /// <summary>ポップアップ (モーダル) 用に、レイアウトなしで本文だけを返す。</summary>
    public IActionResult OnGetFragment([FromServices] HelpCatalog catalog) => Load(catalog, fragment: true);

    private IActionResult Load(HelpCatalog catalog, bool fragment)
    {
        var nav = catalog.GetNavigation(TopicId);
        if (nav is null) return NotFound();
        Navigation = nav;

        RelatedTopics = LinkedTopics.Concat(PopupTopics).Distinct(StringComparer.OrdinalIgnoreCase)
            .Select(catalog.Find).OfType<TopicInfo>().ToList();
        ReferencedBy = catalog.ReferencesTo(TopicId)
            .Where(t => !RelatedTopics.Contains(t)).ToList();
        Keywords = catalog.KeywordsFor(TopicId);

        ViewData["Title"] = nav.Topic.Title;
        ViewData["TopicId"] = TopicId;
        ViewData["Stylesheet"] = Stylesheet;
        ViewData["Fragment"] = fragment;
        return Page();
    }
}
