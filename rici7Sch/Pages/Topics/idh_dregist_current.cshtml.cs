// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>現在の設定をワンクリックプリセットに登録ダイアログボックス (Register Current Settings as One Click Preset Dialog Box)</summary>
public class idh_dregist_currentModel : TopicPageModel
{
    public override string TopicId => "idh_dregist_current";
    public override string SourceFile => "HTMLFILE/idh_dregist_current.htm";
    public override string OriginalTitle => "Register Current Settings as One Click Preset Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("name", "ワンクリックプリセット名：", 2),
        new("comment", "コメント：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];
}
