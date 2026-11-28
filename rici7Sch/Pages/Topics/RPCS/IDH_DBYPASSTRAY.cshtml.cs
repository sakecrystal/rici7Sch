// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>手差しトレイの用紙の位置ダイアログボックス (Paper Position in Bypass Tray Dialog Box)</summary>
public class IDH_DBYPASSTRAYModel : TopicPageModel
{
    public override string TopicId => "RPCS/IDH_DBYPASSTRAY";
    public override string SourceFile => "HTMLFILE/RPCS/IDH_DBYPASSTRAY.htm";
    public override string OriginalTitle => "Paper Position in Bypass Tray Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("define", "手差しトレイの用紙位置の設定", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];
}
