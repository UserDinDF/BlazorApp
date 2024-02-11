using BlazorAppServer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Blazored.LocalStorage;
using static System.Formats.Asn1.AsnWriter;
using MudBlazor.Services;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using BlazorAppServer.Areas.Identity.Data;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Components.Server;
using BlazorAppServer.Data.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Elfie.Serialization;
using BlazorAppServer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using AspNetCore.SEOHelper;
using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("BlazorAppServerContextConnection") ?? throw new InvalidOperationException("Connection string 'BlazorAppServerContextConnection' not found.");

string signalRConnectionString = "Endpoint=https://blazoappsite.service.signalr.net;AccessKey=JYMBkoqcuddjCNQgTwPtTwZRYjLpB2m6WMzRTr1Dqyo=;Version=1.0;";
//builder.Services.AddSignalR().AddAzureSignalR(signalRConnectionString);


//var keyVaultEndpoint = new Uri(builder.Configuration["VaultKey"]);
//var secretClient = new SecretClient(keyVaultEndpoint, new DefaultAzureCredential());
//KeyVaultSecret kvs = secretClient.GetSecret("BlazorApp-secret");

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();

//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(kvs.Value));

var item = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddQuickGridEntityFrameworkAdapter();;


builder.Services.AddIdentity<BlazorAppServerUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

//builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

//builder.Services.AddDefaultIdentity<BlazorAppServerUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddRazorComponents()
//    .AddInteractiveServerComponents();

builder.Services.AddWebEncoders(encoders =>
{
    encoders.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
});

builder.Services.AddMudServices();

builder.Services.AddLocalization();

builder.Services.AddScoped<FileRepository>();
builder.Services.AddScoped<CommentRepository>();
builder.Services.AddScoped<ImageRepository>();
builder.Services.AddScoped<SitemapService>();
builder.Services.AddScoped<FileService>();



//builder.Services.AddRazorPages(options =>
//{
//    options.Conventions.AddPageRoute("/Sitemap", "Sitemap.xml");
//});

builder.Services.AddAuthentication();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole",
        policy => policy.RequireRole("Administrator"));
});

var app = builder.Build();

var webHostEnvironment = app.Services.GetRequiredService<IWebHostEnvironment>();
var sitemapPath = Path.Combine(webHostEnvironment.WebRootPath, "sitemap.xml");
var sitemapContent = await GenerateSitemapXml();
await File.WriteAllTextAsync(sitemapPath, sitemapContent);

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    try
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await CreateRoles(serviceProvider);
        await AssignRole(serviceProvider, roleManager, "daniil.fominykh23@gmail.com", "Administrator");
    }
    catch (Exception ex)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while creating roles or assigning roles.");
    }
}


//using (var scope = builder.Services.BuildServiceProvider().CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    dbContext.Database.Migrate();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    var host = context.Request.Host.Host;
    if (host.Equals("softportal.azurewebsites.net", StringComparison.OrdinalIgnoreCase))
    {
        var newUrl = $"https://www.soft-portal.org{context.Request.Path}{context.Request.QueryString}";
        context.Response.Redirect(newUrl, permanent: true); 
    }
    else
    {
        await next();
    }
});


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


async Task CreateRoles(IServiceProvider serviceProvider)
{
    var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "Administrator", "User", "Moderator" };
    IdentityResult roleResult;

    foreach (var roleName in roleNames)
    {
        var roleExist = await RoleManager.RoleExistsAsync(roleName);
        if (!roleExist)
        {
            roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}
async Task AssignRole(IServiceProvider serviceProvider, RoleManager<IdentityRole> roleManager, string email, string role)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<BlazorAppServerUser>>();
    BlazorAppServerUser user = await userManager.FindByEmailAsync(email);
    if (user != null)
    {
        var result = await userManager.AddToRoleAsync(user, role);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Error assigning role '{role}' to user '{email}': {result.Errors.FirstOrDefault()?.Description}");
        }
    }
    else
    {
        throw new InvalidOperationException($"User '{email}' not found.");
    }
}

async Task<string> GenerateSitemapXml()
{
    StringBuilder stringBuilder = new StringBuilder();
    using (var scope = builder.Services.BuildServiceProvider().CreateScope())
    {
        var fileService = scope.ServiceProvider.GetRequiredService<FileService>();
        var files = await fileService.fileRepository.GetLoadsAsync();

        using (var xmlWriter = XmlWriter.Create(stringBuilder, new XmlWriterSettings { Indent = true }))
        {
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

            foreach (var item in files)
            {
                xmlWriter.WriteStartElement("url");
                xmlWriter.WriteElementString("loc", $"https://soft-portal.org{item.GetFilePath()}");
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
        }
    }

    return stringBuilder.ToString();
}