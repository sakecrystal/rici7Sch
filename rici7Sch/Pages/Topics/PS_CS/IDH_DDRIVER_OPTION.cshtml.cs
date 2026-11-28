// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics.PS_CS;

/// <summary>ドライバーオプション (Driver Options)</summary>
public class IDH_DDRIVER_OPTIONModel : TopicPageModel
{
    public override string TopicId => "PS_CS/IDH_DDRIVER_OPTION";
    public override string SourceFile => "HTMLFILE/PS_CS/IDH_DDRIVER_OPTION.htm";
    public override string OriginalTitle => "Driver Options";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("default", "初期値に戻す", 2),
        new("spool", "EMF 形式でスプールする", 2),
        new("jobload", "ジョブ処理の分担：", 2),
        new("emulation", "直前のエミュレーションに戻す", 2),
        new("near", "近い定形サイズ", 2),
        new("appzoom", "アプリケーションの拡大縮小を使用する", 2),
        new("dmcolor", "アプリケーションの dmColor 設定に常に COLOR を適用する", 2),
        new("preview", "プレビューと印刷画面を表示する", 2),
        new("reduce", "自動縮小：", 2),
        new("profile", "カラープロファイル：", 2),
    ];
}
