using System.Text;
using Iduff.Contracts;
using Iduff.Models;
using Iduff.Repositories;
using Iduff.Services;
using Iduff.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<IduffContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("IduffDb"))
    );


//Adicionando Identity
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<IduffContext>()
    .AddSignInManager()
    .AddRoles<IdentityRole>();

//Adicionando JWT
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
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt: Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

//Adicionando autenticacao para o swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IEventoRepository, EventoRepository>();
builder.Services.AddScoped<ICertificadoService, CertificadoService>();
builder.Services.AddScoped<ICargaHorariaService, CargaHorariaService>();
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<IAlunoService, AlunoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();


app.Run();
