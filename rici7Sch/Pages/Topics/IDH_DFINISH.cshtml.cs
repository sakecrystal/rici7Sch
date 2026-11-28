// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>仕上げ (Finishing)</summary>
public class IDH_DFINISHModel : TopicPageModel
{
    public override string TopicId => "IDH_DFINISH";
    public override string SourceFile => "HTMLFILE/IDH_DFINISH.htm";
    public override string OriginalTitle => "Finishing";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("default", "初期値に戻す", 2),
        new("staple", "ステープル：", 2),
        new("punch", "パンチ：", 2),
        new("fold", "折り：", 2),
        new("multisheet", "複数枚折り", 2),
        new("ring", "リング製本：", 2),
        new("perfect", "無線とじ", 2),
        new("perfectdetails", "詳細...", 2),
        new("foreedge", "小口断裁", 2),
        new("foreedgedetails", "詳細...", 2),
        new("extended", "拡張オプション：", 2),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["idh_how_foldtype", "IDH_HOW_RING", "IDH_HOW_PERFECT_BINDING"];
}
