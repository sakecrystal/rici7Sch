using rici7Sch.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSingleton<HelpCatalog>();
builder.Services.AddSingleton<HelpSourceService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

// 404 などのステータスコードも Error ページで表示する
app.UseStatusCodePagesWithReExecute("/Error", "?code={0}");

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

// 旧 RoboHelp の URL (HTMLFILE/xxx.htm) からの互換リダイレクト
app.MapGet("/HTMLFILE/{**path}", (string path, HelpCatalog catalog) =>
{
    var id = Path.ChangeExtension(path, null)?.Replace('\\', '/') ?? "";
    var topic = catalog.Find(id);
    return topic is null ? Results.NotFound() : Results.LocalRedirect("~" + topic.Url, permanent: true);
});

app.Run();
