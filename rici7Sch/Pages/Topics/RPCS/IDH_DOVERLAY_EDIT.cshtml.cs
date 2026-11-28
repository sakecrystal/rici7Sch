// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>オーバーレイの編集ダイアログボックス (Edit Overlay Dialog Box)</summary>
public class IDH_DOVERLAY_EDITModel : TopicPageModel
{
    public override string TopicId => "RPCS/IDH_DOVERLAY_EDIT";
    public override string SourceFile => "HTMLFILE/RPCS/IDH_DOVERLAY_EDIT.htm";
    public override string OriginalTitle => "Edit Overlay Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("overlay", "オーバーレイデータ：", 2),
        new("delete", "削除", 2),
        new("name", "名前：", 2),
        new("path", "パス/ファイル名：", 2),
        new("browse", "参照...", 2),
        new("add", "追加", 2),
        new("update", "更新", 2),
        new("front", "前面へ移動", 2),
        new("firstpage", "先頭ページのみ", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["RPCS/IDH_HOW_OVERLAY"];
}
