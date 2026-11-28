// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>原稿の向きの混在を回転ダイアログボックス (Rotate Mixed Original Orientation Dialog Box)</summary>
public class IDH_DMIXModel : TopicPageModel
{
    public override string TopicId => "RPCS/IDH_DMIX";
    public override string SourceFile => "HTMLFILE/RPCS/IDH_DMIX.htm";
    public override string OriginalTitle => "Rotate Mixed Original Orientation Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("result", "印刷結果：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("default", "初期値に戻す", 2),
        new("help", "ヘルプ", 2),
    ];
}
