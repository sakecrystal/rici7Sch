// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>両面/レイアウト/製本 (2 Sided/Layout/Booklet)</summary>
public class IDH_DDUPLEXModel : TopicPageModel
{
    public override string TopicId => "IDH_DDUPLEX";
    public override string SourceFile => "HTMLFILE/IDH_DDUPLEX.htm";
    public override string OriginalTitle => "2 Sided/Layout/Booklet";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("default", "初期値に戻す", 2),
        new("layout", "レイアウト：", 2),
        new("layoutorder", "ページ順：", 2),
        new("drawborder", "境界線を印刷する", 2),
        new("layoutmix", "原稿の向きの混在...", 2),
        new("2sided", "両面：", 2),
        new("margins", "とじしろ...", 2),
        new("booklet", "製本：", 2),
        new("bookletorder", "ページ順：", 2),
        new("bookletmix", "原稿の向きの混在...", 2),
        new("creep", "クリープ補正...", 2),
        new("nonreduction", "［用紙に合わせる］を自動で行わない", 2),
        new("area", "印刷可能領域：", 2),
        new("adjust", "画像位置の設定...", 2),
        new("blankpages", "白紙ページを印刷しない", 2),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_HOW_LAYOUT", "IDH_HOW_BOOKLET", "RPCS/IDH_HOW_CREEP"];
}
