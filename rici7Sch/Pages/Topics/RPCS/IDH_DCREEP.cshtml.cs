// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>クリープ補正ダイアログボックス (Creep Adjustment Dialog Box)</summary>
public class IDH_DCREEPModel : TopicPageModel
{
    public override string TopicId => "RPCS/IDH_DCREEP";
    public override string SourceFile => "HTMLFILE/RPCS/IDH_DCREEP.htm";
    public override string OriginalTitle => "Creep Adjustment Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("adjust", "クリープを調整する", 2),
        new("unit", "単位：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("default", "初期値に戻す", 2),
        new("help", "ヘルプ", 2),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["RPCS/IDH_HOW_CREEP"];
}
