// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>面付け (Imposition)</summary>
public class idh_dimpositionModel : TopicPageModel
{
    public override string TopicId => "idh_dimposition";
    public override string SourceFile => "HTMLFILE/idh_dimposition.htm";
    public override string OriginalTitle => "Imposition";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("default", "初期値に戻す", 2),
        new("poster", "ポスター：", 2),
        new("pamphlet", "パンフレット：", 2),
        new("speed", "面付け（スピード）：", 2),
        new("double", "面付け（2面）：", 2),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["RPCS/idh_how_poster", "RPCS/idh_how_pamphlet", "RPCS/IDH_HOW_IMPOSITION"];
}
