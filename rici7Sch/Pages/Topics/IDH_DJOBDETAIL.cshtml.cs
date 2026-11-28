// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>印刷方法の詳細ダイアログボックス (Job Type Details Dialog Box)</summary>
public class IDH_DJOBDETAILModel : TopicPageModel
{
    public override string TopicId => "IDH_DJOBDETAIL";
    public override string SourceFile => "HTMLFILE/IDH_DJOBDETAIL.htm";
    public override string OriginalTitle => "Job Type Details Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("userid", "ユーザー ID：", 2),
        new("enter", "ユーザー ID を入力：", 2),
        new("filename", "ファイル名：", 2),
        new("password", "パスワード：", 2),
        new("username", "ユーザー名：", 2),
        new("time", "印刷時刻を指定する", 2),
        new("fnumber", "フォルダー番号：", 2),
        new("fpass", "フォルダーパスワード：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];
}
