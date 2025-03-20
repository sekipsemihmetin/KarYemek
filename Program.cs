using BLL.AbstractServices.ICategoryServices;
using BLL.AbstractServices.IDailyMenuServices;
using BLL.AbstractServices.IFoodCategoryServices;
using BLL.AbstractServices.IFoodServices;

using BLL.ConcreteServices.CategoryServices;
using BLL.ConcreteServices.DailyMenuServices;
using BLL.ConcreteServices.FoodCategoryServices;
using BLL.ConcreteServices.FoodServices;

using DAL.Abstract_Repositories;
using DAL.Concrete_Repositories;
using DAL.Data;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext and Identity services correctly
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Add Identity after DbContext configuration
builder.Services.AddIdentity<User,IdentityRole> ()
    .AddEntityFrameworkStores<AppDbContext> ()
    .AddDefaultTokenProviders ();

// Add other services
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<IFoodService,FoodService>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IDailyMenuService, DailyMenuService>();
builder.Services.AddScoped<IFoodCategoryService, FoodCategoryService>();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
