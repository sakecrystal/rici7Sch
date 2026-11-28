// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>出力設定 (Output Settings)</summary>
public class idh_ddeliverModel : TopicPageModel
{
    public override string TopicId => "idh_ddeliver";
    public override string SourceFile => "HTMLFILE/idh_ddeliver.htm";
    public override string OriginalTitle => "Output Settings";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("default", "初期値に戻す", 2),
        new("outputtray", "排紙先：", 2),
        new("collate", "ソート：", 2),
        new("collatetype", "ソートの詳細...", 2),
        new("mix", "原稿の向きの混在を回転...", 2),
        new("reverse", "逆順印刷", 2),
        new("faceup", "表向きで排紙", 2),
        new("crops", "トンボを印刷する", 2),
        new("cropsdetails", "詳細...", 2),
        new("stamp", "本体のプリンター設定の［スタンプ］を有効にする", 2),
        new("insert", "仕切り紙を挿入する", 1),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_HOW_COLLATE", "idh_toc_faceup"];
}
