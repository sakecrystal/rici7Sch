// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>給紙トレイを選択する (Selecting the Paper Tray)</summary>
public class IDH_HOW_OUTPUTTRAYModel : TopicPageModel
{
    public override string TopicId => "RPCS/IDH_HOW_OUTPUTTRAY";
    public override string SourceFile => "HTMLFILE/RPCS/IDH_HOW_OUTPUTTRAY.htm";
    public override string OriginalTitle => "Selecting the Paper Tray";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_DSETUPOPTION"];
}
