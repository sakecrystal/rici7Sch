// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>手差しトレイの用紙種類ダイアログボックス (Paper Type in Bypass Tray Dialog Box)</summary>
public class IDH_DPAPERTYPEBYPASSModel : TopicPageModel
{
    public override string TopicId => "RPCS/IDH_DPAPERTYPEBYPASS";
    public override string SourceFile => "HTMLFILE/RPCS/IDH_DPAPERTYPEBYPASS.htm";
    public override string OriginalTitle => "Paper Type in Bypass Tray Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("type", "手差しトレイの用紙種類：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];
}
