using InventryOrderManagementSystem.BLL.Helpers;
using InventryOrderManagementSystem.BLL.Helpers.JWT;
using InventryOrderManagementSystem.BLL.Security;
using InventryOrderManagementSystem.BLL.Services;
using InventryOrderManagementSystem.BLL.SeviceInterfaces;
using InventryOrderManagementSystem.BLL.BackgroundServices;
using InventryOrderManagementSystem.DAL.Data;
using InventryOrderManagementSystem.DAL.Repositories;
using InventryOrderManagementSystem.DAL.RepositoryInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using InventryOrderManagementSystem.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false));
});

builder.Services.AddEndpointsApiExplorer();

//Add Authentication Here
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


//Add Swagger Here
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    // Add JWT authentication
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer {your JWT token}\"",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
});

//Add DbContext Here
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Add HostedService Here
builder.Services.AddHostedService<ReorderAlertService>();
builder.Services.AddHostedService<OrderCleanUpService>();

//Middlewares Here
builder.Services.AddScoped<GlobalExceptionHandler>();
builder.Services.AddScoped<UserIdMiddleware>();

//Add Caching Here
builder.Services.AddMemoryCache();

//Add Logging Here
builder.Logging.ClearProviders(); 
builder.Logging.AddConsole();

//Mapper Configuration Here
builder.Services.AddScoped<AutoMapperProfile>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//Add Services Here
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IReportsServices, ReportsServices>();

//other services Here
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtHelper, JwtHelper>();

//Add Repositories Here
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IReportsRepository, ReportsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandler>();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<UserIdMiddleware>();

app.MapControllers();

app.Run();
