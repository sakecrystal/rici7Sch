// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>印刷方法/コードのダイアログ設定ダイアログボックス (Job Type/Code Dialog Settings Dialog Box)</summary>
public class idh_dadvanced_printingModel : TopicPageModel
{
    public override string TopicId => "idh_dadvanced_printing";
    public override string SourceFile => "HTMLFILE/idh_dadvanced_printing.htm";
    public override string OriginalTitle => "Job Type/Code Dialog Settings Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("classificationcode", "印刷前に［分類コード］ダイアログを表示する", 2),
        new("dispusercode", "印刷前に［ユーザーコード］ダイアログを表示する", 2),
        new("jobtypedetails", "印刷前に［印刷方法の変更］ダイアログを表示する", 2),
        new("changejobtype", "［印刷方法の変更］ダイアログの設定", 1),
        new("savesetvalue", "設定値を保存する", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("default", "初期値に戻す", 2),
        new("help", "ヘルプ", 2),
    ];
}
