using congestion;
using congestion_tax_calculator_domain;
using congestion_tax_calculator_validation;
using congestion_tax_calculator_web_api.Extensions;
using Framework.Common.SwaggerExtensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Logs.ConfigureLogging();
builder.Host.UseSerilog();            


builder.Services.AddDbContext<TaxCalculatorContext>(b =>
{
    b.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        options => { options.CommandTimeout(120); });
});
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICongestionTaxCalculatorService, CongestionTaxCalculatorService>();
builder.Services.AddScoped<IValidationCityService, ValidationCityService>();
builder.Services.AddScoped<IValidationVehicleService, ValidationVehicleService>();


builder.Services.AddControllersWithViews();

builder.Services.AddCustomSwagger();
builder.Services.AddHttpContextAccessor();



var app = builder.Build();

using var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();
var serverDbContext = serviceScope.ServiceProvider.GetRequiredService<TaxCalculatorContext>();
await serverDbContext.Database.MigrateAsync();


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
app.UseCustomSwagger();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
