using SchPeoManageWeb.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 使用内存缓存
builder.Services.AddMemoryCache();

builder.Services.AddMasaBlazor(options =>
{
    options.Locale = new Masa.Blazor.Locale("zh-CN");
    options.ConfigureSsr(ssr =>
    {
        ssr.Left = 256;
        ssr.Top = 64;
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles(); // 允许使用静态文件
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
