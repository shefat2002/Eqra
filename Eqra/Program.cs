using System.Text;
using Eqra.Features.DataAccess.DBContext;
using Eqra.Features.DataAccess.Repositories;
using Eqra.Features.DataAccess.RepositoryInterface;
using Eqra.Features.ServiceImplementation;
using Eqra.Features.ServiceImplementation.AuthService;
using Eqra.Features.ServiceImplementation.ServiceInterface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Jwt 
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

        };
    });
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserLoginService, UserLoginService>();
builder.Services.AddTransient<IUserRegisterService, UserRegisterService>();


builder.Services.AddControllers();

//enable logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
   
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage(); // Enables full stack traces
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
