// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>指定ページ/章区切り機能を使う (Using Designate/Chapter Function)</summary>
public class IDH_HOW_CHAPTERModel : TopicPageModel
{
    public override string TopicId => "IDH_HOW_CHAPTER";
    public override string SourceFile => "HTMLFILE/IDH_HOW_CHAPTER.htm";
    public override string OriginalTitle => "Using Designate/Chapter Function";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["idh_ddesignate"];

    /// <summary>ポップアップで開くトピック</summary>
    public override IReadOnlyList<string> PopupTopics { get; } = ["PCL_CS/IDH_CCHAPTER"];
}
