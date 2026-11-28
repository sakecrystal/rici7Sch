// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>ワンクリックプリセット一覧の管理ダイアログボックス（プリンターのプロパティ） (Manage One Click Preset List Dialog Box (Printer Properties))</summary>
public class idh_dmanage_oneclick_advancedModel : TopicPageModel
{
    public override string TopicId => "idh_dmanage_oneclick_advanced";
    public override string SourceFile => "HTMLFILE/idh_dmanage_oneclick_advanced.htm";
    public override string OriginalTitle => "Manage One Click Preset List Dialog Box (Printer Properties)";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("list", "ワンクリックプリセット一覧：", 2),
        new("up", "上へ", 2),
        new("down", "下へ", 2),
        new("delete", "削除", 2),
        new("restore", "復元...", 2),
        new("share", "共有", 2),
        new("import", "インポート...", 2),
        new("export", "エクスポート...", 2),
        new("name", "ワンクリックプリセット名：", 2),
        new("comment", "コメント：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];
}
