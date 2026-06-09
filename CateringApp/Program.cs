using CateringApp.Data;
using CateringApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Kitchen — always the same
builder.Services.AddScoped<IKitchenFactory, RestaurantKitchenFactory>();

// Fulfillment — swap based on service context
builder.Services.AddScoped<IFulfillmentStrategy, RestaurantFulfillment>();
// or
builder.Services.AddScoped<IFulfillmentStrategy, CateringFulfillment>();

builder.Services.AddScoped<DishService>();

builder.Services.AddDbContext<MyAppContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
