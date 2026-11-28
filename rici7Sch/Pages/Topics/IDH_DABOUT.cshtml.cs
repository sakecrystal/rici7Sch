// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>バージョン情報ダイアログボックス (About Dialog Box)</summary>
public class IDH_DABOUTModel : TopicPageModel
{
    public override string TopicId => "IDH_DABOUT";
    public override string SourceFile => "HTMLFILE/IDH_DABOUT.htm";
    public override string OriginalTitle => "About Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("obtain", "ドライバー設定の保存...", 2),
        new("ok", "OK", 2),
    ];
}
