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

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("BlazorAppServerContextConnection") ?? throw new InvalidOperationException("Connection string 'BlazorAppServerContextConnection' not found.");

string signalRConnectionString = "Endpoint=https://blazoappsite.service.signalr.net;AccessKey=JYMBkoqcuddjCNQgTwPtTwZRYjLpB2m6WMzRTr1Dqyo=;Version=1.0;";
builder.Services.AddSignalR().AddAzureSignalR(signalRConnectionString);


//var keyVaultEndpoint = new Uri(builder.Configuration["VaultKey"]);
//var secretClient = new SecretClient(keyVaultEndpoint, new DefaultAzureCredential());
//KeyVaultSecret kvs = secretClient.GetSecret("BlazorApp-secret");

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();

//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(kvs.Value));

string localdb = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=softportaldb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(localdb));

builder.Services.AddQuickGridEntityFrameworkAdapter();;


//builder.Services.AddDefaultIdentity<BlazorAppServerUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<BlazorAppServerUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddMudServices();

builder.Services.AddLocalization();

builder.Services.AddScoped<FileService>();


var app = builder.Build();

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

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
