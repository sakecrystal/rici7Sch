// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>画質/色の調整タブ (Adjust Image Quality/Color Tab)</summary>
public class idh_qualitycolorModel : TopicPageModel
{
    public override string TopicId => "RPCS/idh_qualitycolor";
    public override string SourceFile => "HTMLFILE/RPCS/idh_qualitycolor.htm";
    public override string OriginalTitle => "Adjust Image Quality/Color Tab";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("profile", "カラープロファイル：", 2),
        new("dither", "ディザリング：", 2),
        new("gray", "グレーの再現：", 2),
        new("colormethod", "カラープロファイルの方法：", 2),
        new("intent", "ICM の目的：", 2),
        new("default", "初期値に戻す", 2),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_HOW_COLORPROFILE", "IDH_HOW_ICM", "IDH_HOW_DITHERING"];
}
