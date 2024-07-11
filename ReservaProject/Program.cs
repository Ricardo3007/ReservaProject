using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReservaProject.Applications;
using ReservaProject.Applications.Contrats;
using ReservaProject.Domain;
using ReservaProject.Domain.Contrats;
using ReservaProject.DTo;
using ReservaProject.Infraestructura.Context;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<ReservasContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionReserva")));

builder.Services.AddTransient<IServicioReservadoService, ServicioReservadoService>();
builder.Services.AddTransient<IServicioReservadoDomain, ServicioReservadoDomain>();
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddTransient<IUsuarioDomain, UsuarioDomain>();


var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Load JWT settings from configuration
var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();
builder.Services.AddSingleton(jwtSettings);


// Add JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        //ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        //ValidIssuer = jwtSettings.Issuer,
        //ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});

// Add authorization
builder.Services.AddAuthorization();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.UseCors(policy =>
{
    policy.WithOrigins("http://localhost:4200") // Reemplaza con la URL de tu aplicación Angular
          .AllowAnyHeader()
          .AllowAnyMethod();
});

app.Run();
