// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>拡張設定タブ (Advanced Options Tab)</summary>
public class IDH_DADVANCED_OPTIONSModel : TopicPageModel
{
    public override string TopicId => "IDH_DADVANCED_OPTIONS";
    public override string SourceFile => "HTMLFILE/IDH_DADVANCED_OPTIONS.htm";
    public override string OriginalTitle => "Advanced Options Tab";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("manage", "管理...", 2),
        new("donot", "ユーザーにプリセットの登録を許可しない", 2),
        new("notchange", "ワンクリックプリセット一覧の表示順の変更を許可しない", 2),
        new("auth2", "ユーザー認証...", 2),
        new("jobtypecode", "印刷方法/コードのダイアログ設定...", 2),
        new("securityset", "セキュリティー設定...", 2),
        new("increase", "アプリケーションごとの設定の自動保持を増やす", 2),
        new("collatedevice", "本体でソート", 2),
        new("notify", "dmPaperLength と dmPaperWidth の両方が非対応であることをアプリケーションに通知する", 2),
        new("custom", "不定形サイズの登録...", 2),
        new("default", "初期値に戻す", 2),
    ];
}
