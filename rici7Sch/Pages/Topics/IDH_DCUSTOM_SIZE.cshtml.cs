// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>不定形サイズダイアログボックス (Custom Paper Size Dialog Box)</summary>
public class IDH_DCUSTOM_SIZEModel : TopicPageModel
{
    public override string TopicId => "IDH_DCUSTOM_SIZE";
    public override string SourceFile => "HTMLFILE/IDH_DCUSTOM_SIZE.htm";
    public override string OriginalTitle => "Custom Paper Size Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("unit", "単位：", 2),
        new("size", "サイズ", 1),
        new("widthlength", "幅：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];
}
