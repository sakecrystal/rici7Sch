// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.PCL_CS;

/// <summary>分類コードダイアログボックス (Classification Code Dialog Box)</summary>
public class idh_dclassModel : TopicPageModel
{
    public override string TopicId => "PCL_CS/idh_dclass";
    public override string SourceFile => "HTMLFILE/PCL_CS/idh_dclass.htm";
    public override string OriginalTitle => "Classification Code Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("classification", "分類コード：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];
}
