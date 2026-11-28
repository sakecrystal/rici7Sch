// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>ヘッダー/フッターの編集ダイアログボックス (Edit Header/Footer Dialog Box)</summary>
public class IDH_DHEADFOOTModel : TopicPageModel
{
    public override string TopicId => "RPCS/IDH_DHEADFOOT";
    public override string SourceFile => "HTMLFILE/RPCS/IDH_DHEADFOOT.htm";
    public override string OriginalTitle => "Edit Header/Footer Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("datetime", "日時", 2),
        new("pageorder", "ページ番号", 2),
        new("text", "文字", 2),
        new("font", "フォント：", 2),
        new("size", "サイズ：", 2),
        new("original", "原稿ページごと", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("default", "初期値に戻す", 2),
        new("help", "ヘルプ", 2),
    ];
}
