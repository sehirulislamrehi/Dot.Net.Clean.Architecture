using CleanArchitecture.DependencyContainers;
using CleanArchitecture.Infrastructure.DatabaseContext;
using CleanArchitecture.Infrastructure.Seeders.Ecommerce.Common;
using CleanArchitecture.Infrastructure.Seeders.Ecommerce.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationEFCoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#region Dependency Resolve
builder.Services.AddEcommerceServices();
#endregion Dependency Resolve


#region DB Seeder Registrations
builder.Services.AddScoped<ISeeder, ModuleSeeder>();
builder.Services.AddScoped<ISeeder, UserSeeder>();
#endregion DB Seeder Registrations

#region Authentication Part
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/";
    });

builder.Services.AddAuthorization();
#endregion Authentication Part End

var app = builder.Build();

#region Run DB Seeder
using (var scope = app.Services.CreateScope())
{
    var seeders = scope.ServiceProvider.GetServices<ISeeder>();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationEFCoreDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    foreach (var seeder in seeders)
    {
        try
        {
            await seeder.SeedAsync(dbContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Seeder failed: {Seeder}", seeder.GetType().Name);
        }
    }
}
#endregion Run DB Seeder


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

#region Authentication Part
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
#endregion Authentication Part End

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=LoginPage}/{id?}");

app.MapRazorPages();

app.Run();
