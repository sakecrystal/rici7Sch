// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>無線とじの詳細ダイアログボックス (Perfect Binding Details Dialog Box)</summary>
public class idh_dperfect_bindingModel : TopicPageModel
{
    public override string TopicId => "idh_dperfect_binding";
    public override string SourceFile => "HTMLFILE/idh_dperfect_binding.htm";
    public override string OriginalTitle => "Perfect Binding Details Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("position", "とじ位置：", 2),
        new("tray", "製本表紙の給紙トレイ：", 2),
        new("cutopt", "無線とじの断裁オプション：", 2),
        new("unit", "単位：", 2),
        new("adjustimage", "画像位置を調整する", 1),
        new("adjustcut", "E. 天：", 2),
        new("finish", "無線とじの仕上がりサイズ：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("default", "初期値に戻す", 2),
        new("help", "ヘルプ", 2),
    ];
}
