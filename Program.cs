using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WEB_SHOW_WRIST_STRAP.Core.Repositories;
using WEB_SHOW_WRIST_STRAP.Repositories;
using WEB_SHOW_WRIST_STRAP.Data;
using WEB_SHOW_WRIST_STRAP.Core;
using WEB_SHOW_WRIST_STRAP.Models.Entities;

var builder = WebApplication.CreateBuilder(args);
var connectionString = WEB_SHOW_WRIST_STRAP.Dataconfig.ConnectStirng_User = builder.Configuration.GetConnectionString("UserLoginContextConnection") ?? throw new InvalidOperationException("Connection string 'UserLoginContextConnection' not found.");
var connectionString2 = WEB_SHOW_WRIST_STRAP.Dataconfig.ConnectString_Data = builder.Configuration.GetConnectionString("PointDataStringConnection") ?? throw new InvalidOperationException("Connection string 'LEDDataStringConnection' not found.");

builder.Services.AddDbContext<UserLoginContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<DataPointContext>(options =>
    options.UseSqlServer(connectionString2));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UserLoginContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Authorization

AddAuthorizationPolicies();

#endregion

AddScoped();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();


void AddAuthorizationPolicies()
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(Constants.Policies.RequireAdmin, policy => policy.RequireRole(Constants.Roles.Administrator));
        options.AddPolicy(Constants.Policies.RequireManager, policy => policy.RequireRole(Constants.Roles.Manager));
    });
}

void AddScoped()
{
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IRoleRepository, RoleRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
}
