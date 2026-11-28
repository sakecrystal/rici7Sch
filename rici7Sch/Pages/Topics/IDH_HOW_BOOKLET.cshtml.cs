// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>冊子に適したレイアウトで印刷する (Printing with a Layout Suitable for Making Booklets)</summary>
public class IDH_HOW_BOOKLETModel : TopicPageModel
{
    public override string TopicId => "IDH_HOW_BOOKLET";
    public override string SourceFile => "HTMLFILE/IDH_HOW_BOOKLET.htm";
    public override string OriginalTitle => "Printing with a Layout Suitable for Making Booklets";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 1;

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_DDUPLEX", "IDH_HOW_LAYOUT"];
}
