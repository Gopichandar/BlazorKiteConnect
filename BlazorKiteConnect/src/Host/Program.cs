﻿using BlazorKiteConnect.Server.Application.Interface;
using BlazorKiteConnect.Server.Configuration;
using BlazorKiteConnect.Server.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.

builder.Services.AddFastEndpoints();
builder.Services.AddRazorPages();

//configuration
builder.Services.Configure<AppDetails>(builder.Configuration.GetSection(nameof(AppDetails)));
builder.Services.Configure<KiteSettings>(builder.Configuration.GetSection("Zerodha"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddHttpClient();

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();


app.MapRazorPages();
app.UseFastEndpoints();
app.MapFallbackToFile("index.html");

app.Run();