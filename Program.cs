using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using ZivotopisCore.Data;
using ZivotopisCore.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ Dynamické načítanie konfigurácie podľa prostredia
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


// ✅ Cookie policy pre bezpečné spracovanie
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
    options.Secure = CookieSecurePolicy.Always;
});

// ✅ Registrácia služieb
builder.Services.AddSession();

builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider();
var provider = builder.Configuration["DatabaseProvider"];
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AplikaciaDbContext>(options =>
{
    if (provider == "PostgreSQL")
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        options.UseSqlServer(connectionString, sqlOptions =>
            sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    }
});

builder.Services.AddScoped<PacientService>();

var app = builder.Build();

// ✅ Middleware konfigurácia
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // bezpečnostný header
}

app.UseHttpsRedirection();
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    await next();
});

app.UseStaticFiles();

app.UseCookiePolicy(); // 💡 aktivuje cookie pravidlá
app.UseSession(); // sem to patrí

app.UseRouting();
app.UseAuthorization();

// ✅ Routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ✅ Automatická migrácia databázy pri štarte
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AplikaciaDbContext>();
    db.Database.Migrate();
}

app.Run();
