// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>ドライバー暗号鍵ダイアログボックス (Driver Encryption Key Dialog Box)</summary>
public class IDH_DDNETWORKKEYModel : TopicPageModel
{
    public override string TopicId => "IDH_DDNETWORKKEY";
    public override string SourceFile => "HTMLFILE/IDH_DDNETWORKKEY.htm";
    public override string OriginalTitle => "Driver Encryption Key Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("key", "ドライバー暗号鍵：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("default", "初期値に戻す", 2),
        new("help", "ヘルプ", 2),
    ];
}
