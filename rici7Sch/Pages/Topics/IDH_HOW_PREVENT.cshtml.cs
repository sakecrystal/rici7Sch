// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>不正コピー抑止機能を使う（地紋による抑止効果） (Using Unauthorized Copy Prevention Function (Effects of Prevention for Pattern))</summary>
public class IDH_HOW_PREVENTModel : TopicPageModel
{
    public override string TopicId => "IDH_HOW_PREVENT";
    public override string SourceFile => "HTMLFILE/IDH_HOW_PREVENT.htm";
    public override string OriginalTitle => "Using Unauthorized Copy Prevention Function (Effects of Prevention for Pattern)";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_HOW_GUARD"];
}
