// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>画像位置の設定ダイアログボックス (Set Image Position Dialog Box)</summary>
public class IDH_DIMGPOSITIONModel : TopicPageModel
{
    public override string TopicId => "RPCS/IDH_DIMGPOSITION";
    public override string SourceFile => "HTMLFILE/RPCS/IDH_DIMGPOSITION.htm";
    public override string OriginalTitle => "Set Image Position Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("shift", "画像位置：", 2),
        new("adjust", "画像位置を調整する", 2),
        new("unit", "単位：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("default", "初期値に戻す", 2),
        new("help", "ヘルプ", 2),
    ];
}
