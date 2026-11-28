// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>効果 (Effects)</summary>
public class IDH_DEFFECTSModel : TopicPageModel
{
    public override string TopicId => "IDH_DEFFECTS";
    public override string SourceFile => "HTMLFILE/IDH_DEFFECTS.htm";
    public override string OriginalTitle => "Effects";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("default", "初期値に戻す", 2),
        new("unauthorized", "不正コピー抑止", 2),
        new("unauthorizededit", "詳細...", 2),
        new("number", "地紋番号", 2),
        new("numberedit", "詳細...", 2),
        new("headerfooter", "ヘッダー/フッター", 2),
        new("headerfooteredit", "詳細...", 2),
        new("watermark", "スタンプを印刷する", 2),
        new("watermarkedit", "編集...", 2),
        new("overlay", "オーバーレイを追加", 2),
        new("overlayedit", "編集...", 2),
        new("bitmap", "ビットマップを追加", 2),
        new("bitmapedit", "編集...", 2),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_HOW_PREVENT", "IDH_HOW_GUARD", "IDH_HOW_BACKGROUNDNUMBERING", "IDH_HOW_WATERMARK"];
}
