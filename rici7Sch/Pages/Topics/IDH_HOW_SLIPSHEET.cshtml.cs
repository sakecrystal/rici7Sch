// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>ページの間に別の用紙を挿入する (Inserting Different Paper between Pages)</summary>
public class IDH_HOW_SLIPSHEETModel : TopicPageModel
{
    public override string TopicId => "IDH_HOW_SLIPSHEET";
    public override string SourceFile => "HTMLFILE/IDH_HOW_SLIPSHEET.htm";
    public override string OriginalTitle => "Inserting Different Paper between Pages";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ポップアップで開くトピック</summary>
    public override IReadOnlyList<string> PopupTopics { get; } = ["PCL_CS/IDH_CSLIP"];
}
