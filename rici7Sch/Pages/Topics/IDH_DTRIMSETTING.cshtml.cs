// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>小口断裁の詳細ダイアログボックス (Fore Edge Cut Details Dialog Box)</summary>
public class IDH_DTRIMSETTINGModel : TopicPageModel
{
    public override string TopicId => "IDH_DTRIMSETTING";
    public override string SourceFile => "HTMLFILE/IDH_DTRIMSETTING.htm";
    public override string OriginalTitle => "Fore Edge Cut Details Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("position", "小口断裁位置の調整：", 2),
        new("unit", "単位：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("default", "初期値に戻す", 2),
        new("help", "ヘルプ", 2),
    ];
}
