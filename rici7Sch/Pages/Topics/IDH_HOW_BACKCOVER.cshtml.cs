// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>裏表紙を異なる種類の用紙に印刷する (Printing the Back Cover on a Different Type of Paper)</summary>
public class IDH_HOW_BACKCOVERModel : TopicPageModel
{
    public override string TopicId => "IDH_HOW_BACKCOVER";
    public override string SourceFile => "HTMLFILE/IDH_HOW_BACKCOVER.htm";
    public override string OriginalTitle => "Printing the Back Cover on a Different Type of Paper";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ポップアップで開くトピック</summary>
    public override IReadOnlyList<string> PopupTopics { get; } = ["PCL_CS/IDH_CBACK"];
}
