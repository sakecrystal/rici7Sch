// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>このプリンタードライバーの特長 (Features of This Printer Driver)</summary>
public class IDH_HOW_ONECLICKModel : TopicPageModel
{
    public override string TopicId => "IDH_HOW_ONECLICK";
    public override string SourceFile => "HTMLFILE/IDH_HOW_ONECLICK.htm";
    public override string OriginalTitle => "Features of This Printer Driver";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_HOW_ONECLICK_NEW", "IDH_HOW_ONECLICK_IMPORT", "IDH_HOW_ONE_ONECLICK_SHARE"];
}
