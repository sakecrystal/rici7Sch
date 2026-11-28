// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>用紙 (Paper)</summary>
public class IDH_DPAPERModel : TopicPageModel
{
    public override string TopicId => "IDH_DPAPER";
    public override string SourceFile => "HTMLFILE/IDH_DPAPER.htm";
    public override string OriginalTitle => "Paper";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("default", "初期値に戻す", 2),
        new("inputtray", "給紙トレイ：", 2),
        new("bypasstray", "手差しトレイでの位置...", 2),
        new("type", "用紙種類：", 2),
        new("tabstock", "［インデックス紙］印刷", 2),
        new("shift", "［インデックス紙］の印字位置をずらす", 2),
        new("letter", "レターヘッド設定：", 2),
        new("apply", "片面印刷にも［レターヘッド設定］を適用する", 2),
    ];
}
