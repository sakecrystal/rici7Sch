// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>オプション構成タブ (Accessories Tab)</summary>
public class IDH_DACCESSModel : TopicPageModel
{
    public override string TopicId => "IDH_DACCESS";
    public override string SourceFile => "HTMLFILE/IDH_DACCESS.htm";
    public override string OriginalTitle => "Accessories Tab";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("image", "本体イメージ", 2),
        new("options", "オプション", 1),
        new("tray", "給紙トレイ設定", 1),
        new("auto", "プリンター情報を自動的に更新する", 2),
        new("updatenow", "今すぐ更新", 2),
        new("default", "初期値に戻す", 2),
    ];
}
