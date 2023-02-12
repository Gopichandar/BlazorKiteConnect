using BlazorKiteConnect.Server.Application.Interface;
using BlazorKiteConnect.Server.Application.Interface.FundsAndMargin;
using BlazorKiteConnect.Server.Application.Interface.Instruments;
using BlazorKiteConnect.Server.Application.Interface.Login;
using BlazorKiteConnect.Server.Application.Interface.Profile;
using BlazorKiteConnect.Server.Configuration;
using BlazorKiteConnect.Server.Persistence;
using BlazorKiteConnect.Server.Services;
using BlazorKiteConnect.Shared.Constants;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.

builder.Services.AddFastEndpoints();
builder.Services.AddRazorPages();

//configuration
builder.Services.Configure<AppDetails>(builder.Configuration.GetSection(nameof(AppDetails)));
builder.Services.Configure<KiteSettings>(builder.Configuration.GetSection("Zerodha"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie((o) =>
                {
                    o.Cookie.Name = AppConstants.CookieName;
                    o.Cookie.HttpOnly = true;
                    o.LoginPath = string.Empty;
                    o.AccessDeniedPath = string.Empty;
                    o.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    };
                });

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddHttpClient();

//services
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IFundsAndMarginService, FundsAndMarginService>();
builder.Services.AddScoped<ILogoutService, LogoutService>();
builder.Services.AddScoped<IInstrumentsService, InstrumentsService>();

//persistance
builder.Services.AddDbContext<KiteAppContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<KiteAppContext>();
    context.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.UseFastEndpoints();
app.MapFallbackToFile("index.html");

app.Run();