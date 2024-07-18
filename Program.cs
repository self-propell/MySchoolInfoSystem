using SchPeoManageWeb.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ʹ���ڴ滺��
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

app.UseStaticFiles(); // ����ʹ�þ�̬�ļ�
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
