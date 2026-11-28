// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>指定ページ/章区切りの詳細ダイアログボックス (Designate/Chapter Details Dialog Box)</summary>
public class idh_ddesignateModel : TopicPageModel
{
    public override string TopicId => "idh_ddesignate";
    public override string SourceFile => "HTMLFILE/idh_ddesignate.htm";
    public override string OriginalTitle => "Designate/Chapter Details Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("number", "ページ番号：", 2),
        new("chaptermode", "印刷モード：", 2),
        new("chaptertray", "給紙トレイ：", 2),
        new("chapterpapertype", "手差しトレイの用紙種類：", 2),
        new("add", "追加", 2),
        new("modify", "変更", 2),
        new("list", "指定ページ/章区切りの設定一覧：", 2),
        new("delete", "削除", 2),
        new("deleteall", "すべて削除", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];
}
