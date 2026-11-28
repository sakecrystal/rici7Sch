// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>カラープロファイルのパターンを変更する (Changing the Color Profiling Pattern)</summary>
public class IDH_HOW_COLORPROFILEModel : TopicPageModel
{
    public override string TopicId => "IDH_HOW_COLORPROFILE";
    public override string SourceFile => "HTMLFILE/IDH_HOW_COLORPROFILE.htm";
    public override string OriginalTitle => "Changing the Color Profiling Pattern";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ポップアップで開くトピック</summary>
    public override IReadOnlyList<string> PopupTopics { get; } = ["IDH_CCOLORPROFILE"];
}
