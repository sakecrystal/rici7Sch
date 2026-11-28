// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>スタンプの編集ダイアログボックス (Edit Watermark Dialog Box)</summary>
public class IDH_DWATERMARKModel : TopicPageModel
{
    public override string TopicId => "IDH_DWATERMARK";
    public override string SourceFile => "HTMLFILE/IDH_DWATERMARK.htm";
    public override string OriginalTitle => "Edit Watermark Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("image", "スタンプのイメージ", 2),
        new("watermark", "スタンプ：", 2),
        new("watermarkdelete", "削除", 2),
        new("name", "名前：", 2),
        new("text", "文字：", 2),
        new("font", "フォント：", 2),
        new("fontstyle", "スタイル：", 2),
        new("color", "カラー：", 2),
        new("frame", "枠/輪郭：", 2),
        new("size", "サイズ：", 2),
        new("angle", "角度：", 2),
        new("density", "濃度：", 2),
        new("add", "追加", 2),
        new("update", "更新", 2),
        new("position", "位置：", 2),
        new("first", "先頭ページのみ", 2),
        new("opaque", "不透明", 2),
        new("repeat", "繰り返し", 2),
        new("original", "原稿ページごと", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_HOW_WATERMARK"];
}
