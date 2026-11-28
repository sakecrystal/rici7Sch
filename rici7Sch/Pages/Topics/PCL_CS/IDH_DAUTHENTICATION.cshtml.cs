// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.PCL_CS;

/// <summary>認証ダイアログボックス (Authentication Dialog Box)</summary>
public class IDH_DAUTHENTICATIONModel : TopicPageModel
{
    public override string TopicId => "PCL_CS/IDH_DAUTHENTICATION";
    public override string SourceFile => "HTMLFILE/PCL_CS/IDH_DAUTHENTICATION.htm";
    public override string OriginalTitle => "Authentication Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("loginname", "ログインユーザー名：", 2),
        new("enterlogin", "ログインユーザー名を入力：", 2),
        new("pass", "ログインパスワード：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("default", "初期値に戻す", 2),
        new("help", "ヘルプ", 2),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["PCL_CS/IDH_HOW_AUTHENTICATION"];
}
