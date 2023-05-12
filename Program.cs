using System.Text;
using Auth_Crud.Database;
using Auth_Crud.Middleware;
using Auth_Crud.Repositories;
using Auth_Crud.Repositories.Implements;
using Auth_Crud.Security;
using Auth_Crud.Services;
using Auth_Crud.Services.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Setting Swagger with jwt
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("JWT Bearer", new OpenApiSecurityScheme
    {
        Description = "This is a JWT bearer authentication scheme",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http
    });
    
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Id = "JWT Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            }, new List<string>()
        }
    });
});

// DI
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddTransient<IPersistence, DbPersistence>();
builder.Services.AddTransient<IAdminRepository, AdminRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ITransactionalRepository, TransactionalRepository>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ITransactionalService, TransactionalService>();
builder.Services.AddTransient<IJwtUtil, JwtUtil>();

// Add Middleware to DI
builder.Services.AddScoped<ExceptionHandlingMiddleware>();

// Add Authentication to DI
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();