// アプリケーションのエントリーポイント (サービス登録とルーティング)
using rici7Sch.Services;

namespace rici7Sch
{
    public partial class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            // 日本語をそのまま (&#x...; にせず) HTML に出力する
            builder.Services.Configure<Microsoft.Extensions.WebEncoders.WebEncoderOptions>(o =>
                o.TextEncoderSettings = new System.Text.Encodings.Web.TextEncoderSettings(System.Text.Unicode.UnicodeRanges.All));
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
        }
    }
}
