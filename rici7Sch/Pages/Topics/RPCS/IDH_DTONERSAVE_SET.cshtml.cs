// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.RPCS;

/// <summary>トナー節約のユーザー設定ダイアログボックス (Toner Save User Settings Dialog Box)</summary>
public class IDH_DTONERSAVE_SETModel : TopicPageModel
{
    public override string TopicId => "RPCS/IDH_DTONERSAVE_SET";
    public override string SourceFile => "HTMLFILE/RPCS/IDH_DTONERSAVE_SET.htm";
    public override string OriginalTitle => "Toner Save User Settings Dialog Box";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("text", "文字：", 2),
        new("ok", "OK", 2),
        new("cancel", "キャンセル", 2),
        new("help", "ヘルプ", 2),
    ];
}
