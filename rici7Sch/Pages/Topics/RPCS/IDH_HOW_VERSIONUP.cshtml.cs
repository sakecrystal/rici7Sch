// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>プリンタードライバーをバージョンアップする (Upgrading the Printer Driver)</summary>
public class IDH_HOW_VERSIONUPModel : TopicPageModel
{
    public override string TopicId => "RPCS/IDH_HOW_VERSIONUP";
    public override string SourceFile => "HTMLFILE/RPCS/IDH_HOW_VERSIONUP.htm";
    public override string OriginalTitle => "Upgrading the Printer Driver";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_HOW_ONECLICK_EXPORT", "IDH_HOW_ONECLICK_IMPORT"];
}
