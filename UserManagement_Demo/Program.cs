using Microsoft.EntityFrameworkCore;
using UserManagement_Demo.Entities;
using UserManagement_Demo.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionStr = builder.Configuration.GetConnectionString("MyCnn");

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(connectionStr));

builder.Services.AddControllersWithViews();

builder.Services.RegisterCustomServices();

var app = builder.Build();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=List}/{id?}"
);

app.Run();
