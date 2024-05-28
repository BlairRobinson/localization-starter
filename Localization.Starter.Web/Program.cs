using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

builder.Services
    .AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, opts => { opts.ResourcesPath = "Resources"; })
    .AddDataAnnotationsLocalization();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-GB"),
        new CultureInfo("cy-GB")
    };

    // State what the default culture for your application is. This will be used if no specific culture
    // can be determined for a given request.
    options.DefaultRequestCulture = new RequestCulture(culture: "en-GB", uiCulture: "en-GB");

    // You must explicitly state which cultures your application supports.
    // These are the cultures the app supports for formatting numbers, dates, etc.
    options.SupportedCultures = supportedCultures;

    // These are the cultures the app supports for UI strings, i.e. we have localized resources for.
    options.SupportedUICultures = supportedCultures;

    // You can change which providers are configured to determine the culture for requests, or even add a custom
    // provider with your own logic. The providers will be asked in order to provide a culture for each request,
    // and the first to provide a non-null result that is in the configured supported cultures list will be used.
    // By default, the following built-in providers are configured:
    // - QueryStringRequestCultureProvider, sets culture via "culture" and "ui-culture" query string values, useful for testing
    // - CookieRequestCultureProvider, sets culture via "ASPNET_CULTURE" cookie
    // - AcceptLanguageHeaderRequestCultureProvider, sets culture via the "Accept-Language" request header
    //options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
    //{
    //  // My custom request culture logic
    //  return new ProviderCultureResult("en");
    //}));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options!.Value);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
