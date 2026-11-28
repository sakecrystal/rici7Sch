// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>セキュリティー設定ダイアログボックス (Security Settings Dialog Box)</summary>
public class idh_dadvanced_securityModel : TopicPageModel
{
    public override string TopicId => "idh_dadvanced_security";
    public override string SourceFile => "HTMLFILE/idh_dadvanced_security.htm";
    public override string OriginalTitle => "Security Settings Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("encryptdata", "機密印刷・保存印刷・保存印刷（共有）のデータを暗号化する", 2),
        new("passnotentered", "パスワードが入力されていない場合", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("default", "初期値に戻す", 2),
        new("help", "ヘルプ", 2),
    ];
}
