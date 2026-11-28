// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>トンボ印刷の詳細ダイアログボックス (Print Crop Marks Details Dialog Box)</summary>
public class IDH_DCROPModel : TopicPageModel
{
    public override string TopicId => "RPCS/IDH_DCROP";
    public override string SourceFile => "HTMLFILE/RPCS/IDH_DCROP.htm";
    public override string OriginalTitle => "Print Crop Marks Details Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("length", "トンボの長さ：", 2),
        new("firstpage", "先頭ページのみ", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("default", "初期値に戻す", 2),
        new("help", "ヘルプ", 2),
    ];
}
