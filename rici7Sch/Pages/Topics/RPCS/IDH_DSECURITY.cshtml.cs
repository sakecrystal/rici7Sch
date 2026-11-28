// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>コピーガードの詳細ダイアログボックス (Data Security for Copying Details Dialog Box)</summary>
public class IDH_DSECURITYModel : TopicPageModel
{
    public override string TopicId => "RPCS/IDH_DSECURITY";
    public override string SourceFile => "HTMLFILE/RPCS/IDH_DSECURITY.htm";
    public override string OriginalTitle => "Data Security for Copying Details Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("image", "コピーガード文字の印刷イメージ", 2),
        new("stringtype", "文字の種類：", 2),
        new("font", "フォント：", 2),
        new("fontstyle", "スタイル：", 2),
        new("size", "サイズ：", 2),
        new("angle", "角度：", 2),
        new("position", "位置：", 2),
        new("repeat", "繰り返し：", 2),
        new("carriage", "行間：", 2),
        new("outline", "袋文字", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("default", "初期値に戻す", 2),
        new("help", "ヘルプ", 2),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_HOW_GUARD"];
}
