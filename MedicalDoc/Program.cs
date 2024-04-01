using MedicalDoc;
using MedicalDoc.DAL.Services;
using MedicalDoc.Models;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("VulnerablePath"));


var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));

//Database connection config
builder.Services.AddDbContext<MDContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(builder.Configuration.GetConnectionString("context"), serverVersion, mysqlOptions =>
        {
            mysqlOptions.EnableRetryOnFailure();
        })
);

builder.Configuration.AddEnvironmentVariables(prefix: "MyCustomPrefix_");

//Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = new PathString("/Account/Login");
    options.ExpireTimeSpan = TimeSpan.FromHours(5);
    options.AccessDeniedPath = new PathString("/Account/AccessDenied");

});

// Set cookies policy
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => false;
    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
});

//Session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    // Set a short timeout for easy testing.
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("LegacyAuth", policy => policy.RequireClaim("LegacyAuth"));
});

builder.Services.AddResponseCaching();

//runtime compilation
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();


builder.Services.AddTransient<AccountService>();
builder.Services.AddTransient<PatientsService>();
builder.Services.AddTransient<ScheduledService>();
builder.Services.AddTransient<InsurancesService>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});


app.UseResponseCaching();
app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
