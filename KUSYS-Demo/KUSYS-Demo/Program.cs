using KUSYS_Demo.Business;
using KUSYS_Demo.Business.Query;
using KUSYS_Demo.Data.Contexts;
using KUSYS_Demo.Entity.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContextFactory<KusysContext>(options => options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString("LocalDB")));
builder.Services.AddIdentity<User, Role>(options =>
{
    // User Password Options
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+$";

}).AddEntityFrameworkStores<KusysContext>();


builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<StudentQuery>();


builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.LoginPath = "/Login/Index";
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Login/Index");
    options.LogoutPath = new PathString("/Login/Index");
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<KusysContext>();

    dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();

public partial class Program { }