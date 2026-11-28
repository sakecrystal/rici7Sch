// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>原稿の向きの混在：製本ダイアログボックス (Mixed Original Orientations: Booklet Dialog Box)</summary>
public class IDH_DMIXBOOKLETModel : TopicPageModel
{
    public override string TopicId => "RPCS/IDH_DMIXBOOKLET";
    public override string SourceFile => "HTMLFILE/RPCS/IDH_DMIXBOOKLET.htm";
    public override string OriginalTitle => "Mixed Original Orientations: Booklet Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("orientation", "印刷結果：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("default", "初期値に戻す", 2),
        new("help", "ヘルプ", 2),
    ];
}
