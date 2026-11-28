// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>コピーガード機能を使う（画像を破壊して灰色で印刷する） (Using Data Security for Copying Function (Destroy the Image and Print Gray))</summary>
public class IDH_HOW_GUARDModel : TopicPageModel
{
    public override string TopicId => "IDH_HOW_GUARD";
    public override string SourceFile => "HTMLFILE/IDH_HOW_GUARD.htm";
    public override string OriginalTitle => "Using Data Security for Copying Function (Destroy the Image and Print Gray)";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_HOW_PREVENT"];
}
