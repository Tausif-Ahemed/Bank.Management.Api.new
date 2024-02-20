using System.Text;
using AutoMapper;
using Bank.Management.Api;
using Bank.Management.Api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

//For Entity Framework
var connectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseMySql(connectionStr, ServerVersion.AutoDetect(connectionStr)));

//For Identity FrameWork
// builder.Services.AddIdentity<IdentityUser, IdentityRole>()
// .AddEntityFrameworkStores<AppDbContext>()
// .AddDefaultTokenProviders();

//For Creating JWT Token Authentication
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
// })
// // Adding Jwt Bearer
// .AddJwtBearer(options =>
// {
//     options.SaveToken = true;
//     options.RequireHttpsMetadata = false;
//     options.TokenValidationParameters = new TokenValidationParameters()
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidAudience = builder.Configuration["JWTAuth:ValidIssuerURL"],
//         ValidIssuer = builder.Configuration["JWTAuth:ValidIssuerURL"],
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTAuth:SecretKey"]))
//     };
// });




// Add services to the container.
builder.Services.AddScoped<IAccountService, AccountServices>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen
( x =>
    x.SwaggerDoc("v1",new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Bank Management Api",
        Version = "v1",
        Description = "This is Besic Bank Management Api",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact {
            Name = "Tausif Ahemed",
            Email = "ABCD@gmail.com",
            Url = new Uri("https://github.com")
        }
    })
);

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

app.Run();
