using System.Text;
using EmployeeManagement_Backend.Context;
using EmployeeManagement_Backend.Helpers;
using EmployeeManagement_Backend.Middleware;
using EmployeeManagement_Backend.Repositories;
using EmployeeManagement_Backend.Security;
using EmployeeManagement_Backend.Services;
using EmployeeManagement_Backend.Services.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ================== Setting Swagger Authentication =================
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name:JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Masukkan autentikasi bearer seperti : `Bearer this-token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[] {}
        }
    });
});

// ================== Setting AppDbContext Connection ================
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"));
});

#region ================ Dependencies ================
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IPersistence, Persistence>();
builder.Services.AddTransient<IJwtUtil, JwtUtil>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ILoginHistoryService, LoginHistoryService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<ICompanyService, CompanyService>();
#endregion

// Middleware
builder.Services.AddScoped<ExceptionHandlingMiddleware>();

// JWT Authentication
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

#region ============== Data Properties =============================
DataProperties.SuccessUpdateMessage = builder.Configuration["GlobalMessages:SuccessUpdateMessage"];
DataProperties.SuccessCreateMessage = builder.Configuration["GlobalMessages:SuccessCreateMessage"];
DataProperties.SuccessDeleteMessage = builder.Configuration["GlobalMessages:SuccessDeleteMessage"];
DataProperties.SuccessGetMessage = builder.Configuration["GlobalMessages:SuccessGetMessage"];
DataProperties.UnauthorizedMessage = builder.Configuration["GlobalMessages:UnauthorizedFailure"];
DataProperties.NotFoundMessage = builder.Configuration["GlobalMessages:NotFoundFailure"];
#endregion

var app = builder.Build();

app.UseCors(policyBuilder =>
    policyBuilder.AllowAnyHeader()
        .SetIsOriginAllowed(origin => true)
        .AllowCredentials());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();