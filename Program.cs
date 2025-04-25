using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CatStore.Components;
using CatStore.Data;
using MudBlazor.Services;
using MudBlazor.Translations;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContextFactory<CatStoreContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ??
                      throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

builder.Services.AddMudServices();
builder.Services.AddMudTranslations();

// AuthenticationStateProvider 是由 AuthorizeView 组件和级联身份验证服务用于获取用户身份验证状态的基础服务。
// 通常不直接使用 AuthenticationStateProvider。
// 请使用 AuthorizeView 组件或 Task<AuthenticationState> 方法。
// 直接使用 AuthenticationStateProvider 的主要缺点是，如果基础身份验证状态数据发生变化，组件不会自动收到通知。

// 在 Program 文件中注册级联身份验证状态服务
// 使用 AuthorizeRouteView 和级联身份验证状态服务设置 Task< AuthenticationState > 级联参数。
builder.Services.AddCascadingAuthenticationState();
// builder.Services.AddScoped<IdentityUserAccessor>();
// builder.Services.AddScoped<IdentityRedirectManager>();
// builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

// 注册身份验证服务所需的服务并配置身份验证选项。
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

// 身份认证数据库相关
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// 为指定的用户类型添加并配置身份系统。默认情况下不添加角色服务，但可以通过 AddRoles() 添加。
builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>() // 添加一个实体框架实现的身份信息存储。
    .AddSignInManager() // 为用户添加登录 API
    .AddDefaultTokenProviders(); // 添加用于生成重置密码、更改电子邮件和更改电话号码操作令牌以及生成双因素身份验证令牌的默认令牌提供程序。

// 为身份认证服务,提供电子邮件发送服务
// builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// 为空数据表填充初始数据
// using 语句确保在填充操作完成后，数据库上下文被释放。
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.AppDataInitialize(services);
    await SeedData.IndividualAuthenticationInitialize(services);
}

// 配置 HTTP 请求管线。
if (app.Environment.IsDevelopment())
{
    // 如果我们尝试访问一个需要访问尚未更新的表的页面或 API 调用，我们会得到一个特殊页面 - 不是错误页面 - 而是一个列出所有尚未应用的迁移的页面。
    // 令人惊讶的是，有一个标记为“应用迁移”（或类似名称）的按钮。我们点击它，然后我们的数据库将根据尚未应用的迁移进行更新。
    // 然后我们重新加载页面，浏览器将显示我们所期望的页面。
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

//添加防伪中间件。
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// 添加身份/账户 Razor 组件所需的其他端点。
app.MapAdditionalIdentityEndpoints();

app.Run();