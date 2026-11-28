// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>よく使う設定タブ (Frequently Used Settings Tab)</summary>
public class IDH_DONECLICKModel : TopicPageModel
{
    public override string TopicId => "IDH_DONECLICK";
    public override string SourceFile => "HTMLFILE/IDH_DONECLICK.htm";
    public override string OriginalTitle => "Frequently Used Settings Tab";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 1;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("image", "印刷イメージ", 2),
        new("list", "原稿サイズ： / 印刷用紙サイズ： / 拡大/縮小：", 2),
        new("summary", "設定内容の確認", 2),
        new("current", "現在の設定を登録...", 2),
        new("iconlist", "ワンクリックプリセット一覧：", 1),
        new("extend", "一覧を拡大 >> / << 一覧を縮小", 2),
        new("manage", "管理...", 2),
        new("jobtype", "印刷方法：", 2),
        new("jobdetail", "詳細...", 2),
        new("docusize", "原稿サイズ：", 2),
        new("printon", "印刷用紙サイズ：", 2),
        new("orient", "向き", 2),
        new("custom", "不定形サイズ...", 2),
        new("type", "用紙種類：", 2),
        new("inputtray", "給紙トレイ：", 2),
        new("layout", "レイアウト：", 2),
        new("layoutorder", "ページ順：", 2),
        new("2sided", "両面：", 2),
        new("booklet", "製本：", 2),
        new("bookletorder", "ページ順：", 2),
        new("staple", "ステープル：", 2),
        new("punch", "パンチ：", 2),
        new("colorbw", "カラー/白黒：", 2),
        new("copies", "部数：", 2),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_HOW_ONECLICK", "IDH_HOW_PROOF_START", "IDH_HOW_SECURE_START", "IDH_HOW_HOLDPRINT", "IDH_HOW_SAVE_PRINTER", "IDH_HOW_STORE_PRINT", "IDH_HOW_DOCUMENT_SERVER", "RPCS/IDH_HOW_OVERLAY", "RPCS/IDH_HOW_IMAGEOVER", "idh_how_custom", "IDH_HOW_LAYOUT", "IDH_HOW_BOOKLET"];
}
