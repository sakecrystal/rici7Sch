// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>不定形サイズの用紙に印刷する (Printing on Custom Size Paper)</summary>
public class idh_how_customModel : TopicPageModel
{
    public override string TopicId => "idh_how_custom";
    public override string SourceFile => "HTMLFILE/idh_how_custom.htm";
    public override string OriginalTitle => "Printing on Custom Size Paper";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_HOW_CUSTOMSIZEREG"];
}
