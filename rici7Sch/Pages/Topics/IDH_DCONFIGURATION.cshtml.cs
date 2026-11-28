// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>構成/バージョン情報タブ (Configuration/About Tab)</summary>
public class IDH_DCONFIGURATIONModel : TopicPageModel
{
    public override string TopicId => "IDH_DCONFIGURATION";
    public override string SourceFile => "HTMLFILE/IDH_DCONFIGURATION.htm";
    public override string OriginalTitle => "Configuration/About Tab";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("tab", "初期表示タブ：", 2),
        new("menu", "詳細設定の初期メニュー：", 2),
        new("type", "ワンクリックプリセット一覧の表示形式：", 2),
        new("property", "プリンターのプロパティ", 2),
        new("about", "バージョン情報", 2),
        new("diagnose", "プリンター診断修正ツール", 2),
        new("wim", "Web Image Monitor...", 2),
    ];
}
