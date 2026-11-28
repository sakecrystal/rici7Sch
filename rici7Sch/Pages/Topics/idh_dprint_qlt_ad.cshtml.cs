// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>印刷品質：詳細 (Print Quality:Advanced)</summary>
public class idh_dprint_qlt_adModel : TopicPageModel
{
    public override string TopicId => "idh_dprint_qlt_ad";
    public override string SourceFile => "HTMLFILE/idh_dprint_qlt_ad.htm";
    public override string OriginalTitle => "Print Quality:Advanced";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("default", "初期値に戻す", 2),
        new("cmyk", "CMYK に分解して指定色のみ印刷する", 2),
        new("rastervector", "ベクター/ラスター：", 2),
        new("thicker", "線幅の調整", 2),
        new("textblack", "すべての文字を黒で印刷する", 2),
        new("blackover", "黒のオーバープリント", 2),
        new("photograph", "写真の調整", 2),
        new("edgesmooth", "エッジスムージング", 2),
        new("fuesr", "定着の設定：", 2),
        new("dataprocess", "データ処理の変更", 2),
        new("ditherbrush", "地紋パターンをずらす", 2),
        new("enlargeditherbrush", "地紋パターンを拡大する", 2),
        new("boldtext", "太字の調整", 2),
        new("textthick", "文字を太くする", 2),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["PS_CS/IDH_HOW_CMYK", "IDH_HOW_BLACK_PRINTING"];
}
