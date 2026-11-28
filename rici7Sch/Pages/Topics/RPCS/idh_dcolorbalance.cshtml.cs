// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>カラーバランスタブ (Color Balance Tab)</summary>
public class idh_dcolorbalanceModel : TopicPageModel
{
    public override string TopicId => "RPCS/idh_dcolorbalance";
    public override string SourceFile => "HTMLFILE/RPCS/idh_dcolorbalance.htm";
    public override string OriginalTitle => "Color Balance Tab";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("preset", "プリセットの呼び出し", 2),
        new("component", "設定方法", 2),
        new("before", "前", 2),
        new("after", "後", 2),
        new("brightness", "明るさ：", 2),
        new("contrast", "コントラスト：", 2),
        new("saturation", "彩度：", 2),
        new("adjustment", "色ごとに調整する", 2),
        new("default", "初期値に戻す", 2),
    ];
}
