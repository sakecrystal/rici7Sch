// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>印刷品質：標準 (Print Quality:Standard)</summary>
public class IDH_DPRINT_QLTModel : TopicPageModel
{
    public override string TopicId => "IDH_DPRINT_QLT";
    public override string SourceFile => "HTMLFILE/IDH_DPRINT_QLT.htm";
    public override string OriginalTitle => "Print Quality:Standard";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("default", "初期値に戻す", 2),
        new("aiqc", "画質/色の調整", 1),
        new("resolution", "解像度：", 2),
        new("tonersave", "トナー節約：", 2),
        new("cud", "カラーユニバーサルデザイン：", 2),
        new("priority", "印刷の優先：", 2),
        new("twocolor", "2色：", 2),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["RPCS/IDH_HOW_TWOCOLOR"];
}
