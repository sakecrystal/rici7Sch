using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace rici7Sch.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    public string? RequestId { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    public int Code { get; set; }
    public string? OriginalPath { get; set; }

    public bool IsNotFound => Code == StatusCodes.Status404NotFound;

    public void OnGet(int? code) => Handle(code);
    public void OnPost(int? code) => Handle(code);

    private void Handle(int? code)
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        var reExecute = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
        OriginalPath = reExecute?.OriginalPath
            ?? HttpContext.Features.Get<IExceptionHandlerPathFeature>()?.Path;
        Code = code ?? (HttpContext.Features.Get<IExceptionHandlerFeature>() is not null ? 500 : Response.StatusCode);
        if (Code < 400) Code = 500;
        Response.StatusCode = Code;
    }
}
