// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>表紙/合紙/指定ページ (Cover/Slip/Designate)</summary>
public class idh_dcoverModel : TopicPageModel
{
    public override string TopicId => "idh_dcover";
    public override string SourceFile => "HTMLFILE/idh_dcover.htm";
    public override string OriginalTitle => "Cover/Slip/Designate";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("default", "初期値に戻す", 2),
        new("front", "表紙", 1),
        new("back", "裏表紙", 1),
        new("slip", "1ページごとに合紙を挿入する", 1),
        new("designate", "指定ページ/章区切り", 1),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_HOW_FRONTCOVER", "IDH_HOW_BACKCOVER", "IDH_HOW_SLIPSHEET", "IDH_HOW_CHAPTER"];
}
