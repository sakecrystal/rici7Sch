// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>ワンクリックプリセット一覧の管理ダイアログボックス（印刷設定） (Manage One Click Preset List Dialog Box (Printing Preferences))</summary>
public class IDH_DMANAGE_ONECLICKModel : TopicPageModel
{
    public override string TopicId => "IDH_DMANAGE_ONECLICK";
    public override string SourceFile => "HTMLFILE/IDH_DMANAGE_ONECLICK.htm";
    public override string OriginalTitle => "Manage One Click Preset List Dialog Box (Printing Preferences)";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("list", "ワンクリックプリセット一覧：", 2),
        new("up", "上へ", 2),
        new("down", "下へ", 2),
        new("delete", "削除", 2),
        new("import", "インポート...", 2),
        new("export", "エクスポート...", 2),
        new("name", "ワンクリックプリセット名：", 2),
        new("comment", "コメント：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];
}
