// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>カラープロファイル設定ダイアログボックス (Color Profile Settings Dialog Box)</summary>
public class IDH_DCOLOR_MATCHModel : TopicPageModel
{
    public override string TopicId => "RPCS/IDH_DCOLOR_MATCH";
    public override string SourceFile => "HTMLFILE/RPCS/IDH_DCOLOR_MATCH.htm";
    public override string OriginalTitle => "Color Profile Settings Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("color", "カラープロファイル：", 2),
        new("delete", "削除", 2),
        new("edit", "名前：", 2),
        new("path", "パス/ファイル名：", 2),
        new("browse", "参照...", 2),
        new("add", "追加", 2),
        new("update", "更新", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];
}
