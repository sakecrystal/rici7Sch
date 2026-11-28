// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>イメージオーバーレイの登録ダイアログボックス (Register Image Overlay Dialog Box)</summary>
public class IDH_DOVERLAYModel : TopicPageModel
{
    public override string TopicId => "RPCS/IDH_DOVERLAY";
    public override string SourceFile => "HTMLFILE/RPCS/IDH_DOVERLAY.htm";
    public override string OriginalTitle => "Register Image Overlay Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("number", "登録番号：", 2),
        new("name", "登録名：", 2),
        new("orientation", "イメージオーバーレイの向き：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];
}
