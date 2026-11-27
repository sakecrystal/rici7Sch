using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using rici7Sch.Services;

namespace rici7Sch.Pages.Classic;

/// <summary>
/// Classic ビューアーのフレームに、元の RoboHelp トピック (HelpSource/HTMLFILE) を表示する。
/// 元の mshtmlExp*.css と ehlpdhtm.js がそのまま使われ、ドロップダウン・ポップアップも元の動作になる。
/// </summary>
public class FrameModel(HelpSourceService source) : PageModel
{
    public IActionResult OnGet(string? path)
    {
        if (string.IsNullOrEmpty(path)) return NotFound();
        var id = Path.ChangeExtension(path, null)?.Replace('\\', '/') ?? "";
        var html = source.ReadOriginalHtml(id);
        if (html is null)
        {
            var alt = source.FindByFileName(id);
            return alt is null ? NotFound() : LocalRedirect("~/Classic/HTMLFILE/" + alt + ".htm");
        }
        // 同一オリジンのフレーム内でのみ表示を許可
        Response.Headers.XFrameOptions = "SAMEORIGIN";
        return Content(html, "text/html; charset=utf-8");
    }
}
