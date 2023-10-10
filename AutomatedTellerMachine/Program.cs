using BusinessLogicLayer;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DataObjectLayer;
using NuGet.Configuration;
using System.Configuration;
using BusinessLogicLayer.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<BLLForCustomerAccount>();
builder.Services.AddScoped(typeof(BLLForAdmin));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<Repository<CustomerAccount>>();
builder.Services.AddScoped<Repository<LoginCredential>>();

// Configuring the base api url
builder.Services.Configure<ApiSettings>(configuration.GetSection("APIUrl"));

builder.Services.AddDbContext<ATMDBContext>(options =>
{
    IServiceCollection services = new ServiceCollection();
    services.AddControllers();
    options.UseSqlServer(configuration.GetConnectionString("ATMDBConnection"));
});

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
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    // c.SwaggerEndpoint("/swagger/v2/swagger.json", "AutomatedTellerMachine";
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
