using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Notes2022.Server.Data;
using Notes2022.Server.Models;
//using Syncfusion.Blazor;
//using Syncfusion.Licensing;
using Microsoft.AspNetCore.Identity;
using Notes2022.Server;
using Notes2022.Server.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Configuration;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

//if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "/SyncfusionLicense.txt"))
//{
//    string licenseKey = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "/SyncfusionLicense.txt");
//    SyncfusionLicenseProvider.RegisterLicense(licenseKey);
//}

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<NotesDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<NotesDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, NotesDbContext>();

//builder.Services.AddSignalR();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Default Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

});

builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization();

builder.Services.AddRazorPages();

//builder.Services.AddSyncfusionBlazor();

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);




//Globals.UserDataList = new List<Notes2022.Shared.UserData>();

Globals.StartupDateTime = DateTime.Now.ToUniversalTime();

Globals.ProductionUrl = builder.Configuration["ProductionUrl"];

Globals.ImportRoot = builder.Configuration["ImportRoot"];

Globals.PathBase = builder.Configuration["PathBase"];

Globals.MigrateDb = builder.Configuration["MigrateDb"];


Globals.TimeZoneDefaultID = 54;   /// int.Parse(builder.Configuration["DefaultTZ"]);  // this would fail during a db migration!!

Globals.SendGridApiKey = builder.Configuration["SendGridApiKey"];
Globals.SendGridEmail = builder.Configuration["SendGridEmail"];
Globals.SendGridName = builder.Configuration["SendGridName"];

Globals.DBConnectString = builder.Configuration.GetConnectionString("DefaultConnection");

Globals.PrimeAdminName = "Dale Sinder";
Globals.PrimeAdminEmail = "sinder@illinois.edu";

try
{
    Globals.PrimeAdminName = builder.Configuration["PrimeAdminName"];
    Globals.PrimeAdminEmail = builder.Configuration["PrimeAdminEmail"];
}
catch
{
    //ignore
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

//app.UsePathBase(Globals.PathBase);

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
