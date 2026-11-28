// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>無線とじで製本する (Binding Documents by Perfect Binding)</summary>
public class IDH_HOW_PERFECT_BINDINGModel : TopicPageModel
{
    public override string TopicId => "IDH_HOW_PERFECT_BINDING";
    public override string SourceFile => "HTMLFILE/IDH_HOW_PERFECT_BINDING.htm";
    public override string OriginalTitle => "Binding Documents by Perfect Binding";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["idh_dperfect_binding"];
}
