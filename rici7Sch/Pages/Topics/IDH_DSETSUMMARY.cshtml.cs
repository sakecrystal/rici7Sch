// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>設定内容の確認ダイアログボックス (Settings Summary Dialog Box)</summary>
public class IDH_DSETSUMMARYModel : TopicPageModel
{
    public override string TopicId => "IDH_DSETSUMMARY";
    public override string SourceFile => "HTMLFILE/IDH_DSETSUMMARY.htm";
    public override string OriginalTitle => "Settings Summary Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("expand", "展開", 2),
        new("collapse", "縮小", 2),
        new("setlist", "一覧：", 2),
        new("image", "本体イメージ", 2),
        new("traylist", "選択したトレイの一覧：", 2),
        new("ok", "OK", 2),
    ];
}
