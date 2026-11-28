// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>困ったときには (Troubleshooting)</summary>
public class IDH_TROUBLESHOOTModel : TopicPageModel
{
    public override string TopicId => "IDH_TROUBLESHOOT";
    public override string SourceFile => "HTMLFILE/IDH_TROUBLESHOOT.htm";
    public override string OriginalTitle => "Troubleshooting";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_DSETUPOPTION"];
}
