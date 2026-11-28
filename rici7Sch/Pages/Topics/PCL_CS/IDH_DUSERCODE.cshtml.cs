// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.PCL_CS;

/// <summary>ユーザーコードダイアログボックス (User Code Dialog Box)</summary>
public class IDH_DUSERCODEModel : TopicPageModel
{
    public override string TopicId => "PCL_CS/IDH_DUSERCODE";
    public override string SourceFile => "HTMLFILE/PCL_CS/IDH_DUSERCODE.htm";
    public override string OriginalTitle => "User Code Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("usercode", "ユーザーコード：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];
}
