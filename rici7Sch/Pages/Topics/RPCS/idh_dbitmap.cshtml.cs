// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>ビットマップの編集ダイアログボックス (Edit Bitmap Dialog Box)</summary>
public class idh_dbitmapModel : TopicPageModel
{
    public override string TopicId => "RPCS/idh_dbitmap";
    public override string SourceFile => "HTMLFILE/RPCS/idh_dbitmap.htm";
    public override string OriginalTitle => "Edit Bitmap Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("image", "ビットマップスタンプのイメージ", 2),
        new("bitmap", "ビットマップ：", 2),
        new("delete", "削除", 2),
        new("name", "名前：", 2),
        new("path", "パス/ファイル名：", 2),
        new("browse", "参照...", 2),
        new("size", "サイズ：", 2),
        new("add", "追加", 2),
        new("update", "更新", 2),
        new("position", "位置：", 2),
        new("firstpage", "先頭ページのみ", 2),
        new("opaque", "不透明", 2),
        new("repeat", "繰り返し", 2),
        new("original", "原稿ページごと", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["RPCS/IDH_HOW_IMAGES"];
}
