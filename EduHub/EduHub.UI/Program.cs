using AutoMapper;
using EduHub.Application.Interfaces;
using EduHub.Application.Mapper;
using EduHub.Application.Services;
using EduHub.Domain.Entities;
using EduHub.Domain.Settings;
using EduHub.Integration.Bootstrap;
using EduHub.Persistence.Abstractions;
using EduHub.Persistence.DataContext;
using EduHub.Persistence.Realizations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DataConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, AppRole>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 4;
    }).AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Settings
var hostSettings = new HostSettings();
builder.Configuration.GetSection("HostSettings").Bind(hostSettings);
builder.Services.AddScoped<HostSettings>(_ => hostSettings);

//UrlSettings
var urlSettings = new UrlSettings();
builder.Configuration.GetSection("UrlSettings").Bind(urlSettings);
builder.Services.AddScoped<UrlSettings>(_ => urlSettings);

//Blob
var blobSettings = new BlobStorageSettings();
builder.Configuration.GetSection("BlobStorageSettings").Bind(blobSettings);
builder.Services.AddScoped<BlobStorageSettings>(_ => blobSettings);

// SendGrid
var sendGridSettings = new SendGridSettings();
builder.Configuration.GetSection("SendGridSettings").Bind(sendGridSettings);
builder.Services.AddScoped<SendGridSettings>(_ => sendGridSettings);


// Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ITestService, TestService>();

//mapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddSingleton(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile(hostSettings));
}).CreateMapper());

builder.Services.RegisterIntegration();


builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

// Logger
var outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm: ss.fff} [{Level}] {Message}{NewLine}{Exception}";
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .WriteTo.File("./Logs/EduHub-Logs-.txt", rollingInterval: RollingInterval.Day, outputTemplate: outputTemplate)
    //.WriteTo.Seq("http://localhost:5341")
    .CreateLogger();

builder.Services.AddControllersWithViews();

builder.Logging.AddSerilog(Log.Logger);

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
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"AppFiles")),
//    RequestPath = new PathString("/AppFiles")
//});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();