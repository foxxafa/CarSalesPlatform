using Application;
using Application.Mappings;
using Application.Validators;
using AutoMapper;
using CarSalesPlatformMVC.Areas.Website.Filters;
using CarSalesPlatformMVC.Areas.Website.Middlewares;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

ValidatorOptions.Global.LanguageManager.Enabled = false;

// Application services
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(UserAuthenticationStateFilter));
});

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);



//builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.StringEscapeHandling = StringEscapeHandling.Default;
                })
                .AddFluentValidation(config =>
                {
                    config.RegisterValidatorsFromAssemblyContaining<PostCarValidator>();
                });
                
                //.ConfigureApiBehaviorOptions(options=> options.SuppressModelStateInvalidFilter=true);



//builder.Services.Configure<ApiBehaviorOptions>(options =>
//{
//    options.SuppressModelStateInvalidFilter = true;
//});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
});

// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Token:Audience"],

        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],

        ValidateLifetime = true,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),

        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
        expires.HasValue && expires.Value > DateTime.UtcNow,

        NameClaimType = ClaimTypes.Name,
        RoleClaimType = ClaimTypes.Role
    };
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

app.UseRouting();

app.UseMiddleware<TokenCookieToHeaderMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "defaultWebsite",
    areaName: "Website",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);


app.MapAreaControllerRoute(
    name: "defaultPanel",
    areaName: "Panel",
    pattern: "{area=Panel}/{controller=Default}/{action=Index}/{id?}"
);


app.Run();
