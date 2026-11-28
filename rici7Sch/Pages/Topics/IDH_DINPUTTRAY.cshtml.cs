// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>給紙トレイ設定の変更ダイアログボックス (Change Input Tray Settings Dialog Box)</summary>
public class IDH_DINPUTTRAYModel : TopicPageModel
{
    public override string TopicId => "IDH_DINPUTTRAY";
    public override string SourceFile => "HTMLFILE/IDH_DINPUTTRAY.htm";
    public override string OriginalTitle => "Change Input Tray Settings Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("inputtray", "給紙トレイ：", 2),
        new("size", "用紙サイズ：", 2),
        new("type", "用紙種類：", 2),
        new("autotray", "自動トレイ選択を使用しない", 2),
        new("position", "用紙の位置", 2),
        new("update", "給紙トレイ/用紙サイズの変更", 2),
        new("list", "給紙トレイ設定一覧：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];
}
