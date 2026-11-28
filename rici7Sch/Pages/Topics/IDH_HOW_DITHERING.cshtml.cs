// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>ディザリングパターンを変更する (Changing the Dithering Pattern)</summary>
public class IDH_HOW_DITHERINGModel : TopicPageModel
{
    public override string TopicId => "IDH_HOW_DITHERING";
    public override string SourceFile => "HTMLFILE/IDH_HOW_DITHERING.htm";
    public override string OriginalTitle => "Changing the Dithering Pattern";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ポップアップで開くトピック</summary>
    public override IReadOnlyList<string> PopupTopics { get; } = ["IDH_CDITHER"];
}
