using Application;
using Application.Validators;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Application services
builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();

builder.Services.AddControllers()
                .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CreateColorValidator>());
                //.ConfigureApiBehaviorOptions(options=> options.SuppressModelStateInvalidFilter=true);

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

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); 
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.Run();
