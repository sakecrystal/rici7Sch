// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>地紋番号の詳細ダイアログボックス (Background Numbering Details Dialog Box)</summary>
public class IDH_DBACKGROUNDModel : TopicPageModel
{
    public override string TopicId => "IDH_DBACKGROUND";
    public override string SourceFile => "HTMLFILE/IDH_DBACKGROUND.htm";
    public override string OriginalTitle => "Background Numbering Details Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("image", "地紋番号の印刷イメージ", 2),
        new("size", "サイズ：", 2),
        new("density", "濃度：", 2),
        new("color", "カラー：", 2),
        new("number", "開始番号：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("default", "初期値に戻す", 2),
        new("help", "ヘルプ", 2),
    ];
}
