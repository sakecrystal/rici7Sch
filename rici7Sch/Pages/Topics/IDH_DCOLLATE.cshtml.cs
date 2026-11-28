// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>ソートの詳細ダイアログボックス (Collate Details Dialog Box)</summary>
public class IDH_DCOLLATEModel : TopicPageModel
{
    public override string TopicId => "IDH_DCOLLATE";
    public override string SourceFile => "HTMLFILE/IDH_DCOLLATE.htm";
    public override string OriginalTitle => "Collate Details Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("type", "ソートの種類：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];
}
