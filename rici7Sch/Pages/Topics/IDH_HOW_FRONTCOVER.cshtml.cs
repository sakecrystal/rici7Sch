// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>1 枚目だけを異なる種類の用紙に印刷する (Printing Only the First Sheet on a Different Type of Paper)</summary>
public class IDH_HOW_FRONTCOVERModel : TopicPageModel
{
    public override string TopicId => "IDH_HOW_FRONTCOVER";
    public override string SourceFile => "HTMLFILE/IDH_HOW_FRONTCOVER.htm";
    public override string OriginalTitle => "Printing Only the First Sheet on a Different Type of Paper";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ポップアップで開くトピック</summary>
    public override IReadOnlyList<string> PopupTopics { get; } = ["PCL_CS/IDH_CFRONT"];
}
