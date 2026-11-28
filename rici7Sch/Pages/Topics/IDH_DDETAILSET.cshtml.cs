// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>詳細設定タブ (Detailed Settings Tab)</summary>
public class IDH_DDETAILSETModel : TopicPageModel
{
    public override string TopicId => "IDH_DDETAILSET";
    public override string SourceFile => "HTMLFILE/IDH_DDETAILSET.htm";
    public override string OriginalTitle => "Detailed Settings Tab";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("image", "印刷イメージ", 2),
        new("list", "原稿サイズ： / 印刷用紙サイズ： / 拡大/縮小：", 2),
        new("summary", "設定内容の確認", 2),
        new("current", "現在の設定を登録...", 2),
        new("menu", "メニュー：", 1),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_DJOBSETUP", "IDH_DBASIC", "IDH_DPAPER", "idh_dcover", "IDH_DDUPLEX", "idh_dimposition", "idh_ddeliver", "IDH_DFINISH", "IDH_DPRINT_QLT", "idh_dprint_qlt_ad", "IDH_DEFFECTS", "PS_CS/IDH_DDRIVER_OPTION"];
}
