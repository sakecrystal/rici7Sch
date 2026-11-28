// このファイルは tools/convert_help.py により生成されています。
namespace rici7Sch.Pages.Topics;

/// <summary>印刷方法 (Job Setup)</summary>
public class IDH_DJOBSETUPModel : TopicPageModel
{
    public override string TopicId => "IDH_DJOBSETUP";
    public override string SourceFile => "HTMLFILE/IDH_DJOBSETUP.htm";
    public override string OriginalTitle => "Job Setup";
    public override string Stylesheet => "mshtmlExpNonbrand";
    public override int DropdownCount => 0;

    /// <summary>ページ内の見出し (このページの内容)</summary>
    public override IReadOnlyList<TopicSection> Sections { get; } =
    [
        new("default", "初期値に戻す", 2),
        new("jobtype", "印刷方法：", 2),
        new("jobdetail", "詳細...", 2),
        new("classification", "分類コード：", 2),
        new("usercode", "ユーザーコード：", 2),
        new("auth", "認証...", 2),
    ];

    /// <summary>本文から参照しているトピック</summary>
    public override IReadOnlyList<string> LinkedTopics { get; } = ["IDH_HOW_PROOF_START", "IDH_HOW_SECURE_START", "IDH_HOW_HOLDPRINT", "IDH_HOW_SAVE_PRINTER", "IDH_HOW_STORE_PRINT", "IDH_HOW_DOCUMENT_SERVER", "RPCS/IDH_HOW_OVERLAY", "RPCS/IDH_HOW_IMAGEOVER", "PCL_CS/IDH_HOW_CLASS", "PCL_CS/IDH_HOW_AUTHENTICATION"];
}
