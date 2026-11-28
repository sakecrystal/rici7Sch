// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>2色で印刷する (Printing in Two Colors)</summary>
public class IDH_HOW_TWOCOLORModel : TopicPageModel
{
    public override string TopicId => "RPCS/IDH_HOW_TWOCOLOR";
    public override string SourceFile => "HTMLFILE/RPCS/IDH_HOW_TWOCOLOR.htm";
    public override string OriginalTitle => "Printing in Two Colors";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["PS_CS/IDH_HOW_CMYK"];
}
