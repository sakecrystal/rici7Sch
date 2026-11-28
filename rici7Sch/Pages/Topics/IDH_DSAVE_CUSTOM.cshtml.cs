// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>不定形サイズの登録ダイアログボックス (Register Custom Paper Size Dialog Box)</summary>
public class IDH_DSAVE_CUSTOMModel : TopicPageModel
{
    public override string TopicId => "IDH_DSAVE_CUSTOM";
    public override string SourceFile => "HTMLFILE/IDH_DSAVE_CUSTOM.htm";
    public override string OriginalTitle => "Register Custom Paper Size Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("list", "不定形サイズ一覧：", 2),
        new("name", "名前：", 2),
        new("unit", "単位：", 2),
        new("widthlength", "幅：", 2),
        new("modify", "変更", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];
}
