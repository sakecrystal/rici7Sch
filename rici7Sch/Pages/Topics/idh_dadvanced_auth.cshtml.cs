// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>ユーザー認証ダイアログボックス (User Authentication Dialog Box)</summary>
public class idh_dadvanced_authModel : TopicPageModel
{
    public override string TopicId => "idh_dadvanced_auth";
    public override string SourceFile => "HTMLFILE/idh_dadvanced_auth.htm";
    public override string OriginalTitle => "User Authentication Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("authonoff", "ユーザー認証", 2),
        new("dispauth", "印刷前に［認証］ダイアログを表示する", 2),
        new("credentials", "Windows の資格情報で印刷前に［認証］ダイアログを表示する", 2),
        new("encryptlogin", "ログインパスワードを暗号化する", 2),
        new("driverencryptkey", "ドライバー暗号鍵：", 2),
        new("reenterkey", "確認キーを再入力：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("default", "初期値に戻す", 2),
        new("help", "ヘルプ", 2),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["PCL_CS/IDH_HOW_AUTHENTICATION"];
}
