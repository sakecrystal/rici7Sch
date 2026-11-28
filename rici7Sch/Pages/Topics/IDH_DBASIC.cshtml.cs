// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>基本 (Basic)</summary>
public class IDH_DBASICModel : TopicPageModel
{
    public override string TopicId => "IDH_DBASIC";
    public override string SourceFile => "HTMLFILE/IDH_DBASIC.htm";
    public override string OriginalTitle => "Basic";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("default", "初期値に戻す", 2),
        new("orient", "向き", 2),
        new("colorbw", "カラー/白黒：", 2),
        new("rotated", "180度回転", 2),
        new("copies", "部数：", 2),
        new("docusize", "原稿サイズ：", 2),
        new("custom", "不定形サイズ...", 2),
        new("reduce", "拡大/縮小：", 2),
        new("zoom", "拡大/縮小：", 2),
        new("magnification", "縦： / 横：", 2),
        new("printon", "印刷用紙サイズ：", 2),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["idh_how_custom", "idh_how_zoom"];
}
