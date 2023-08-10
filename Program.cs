

using Ecommerce.BL.Services;
using Ecommerce.DAL.Data;
using Ecommerce.DAL.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication2.DAL.models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(opts => 
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:dbConnection"]);
    opts.EnableSensitiveDataLogging(true);
});
#region Services BL
builder.Services.AddScoped(typeof(IRepository<>), typeof(GeneralRepository<>));
builder.Services.AddScoped<Iapplydiscountservice, Applieddiscount>();
builder.Services.AddScoped<Idiscountservice, discountservice>();
builder.Services.AddScoped<Iproductservice, productservice>();
builder.Services.AddScoped<IOrderservice, Orderservice>();
builder.Services.AddScoped<Irateservice, Rateservice>();
builder.Services.AddScoped<Icategoryservice, Categoryservice>();
builder.Services.AddScoped<Ishoppingcart, Shoppingcartservice>();
builder.Services.AddScoped<Ishoppingcartitemservice, Shoppingcartitemservice>();
builder.Services.AddHttpContextAccessor();

#endregion 

builder.Services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
builder.Services.AddIdentityCore<User>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options=>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters =
           new TokenValidationParameters()
           {
               ValidateIssuer = true,
               ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
               ValidateAudience = true,
               ValidAudience = builder.Configuration["JWT:ValidAudience"],
               IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecrtKey"]))
           };
});

builder.Services.AddAuthorization();
var app = builder.Build();

app.UseAuthentication();
app.UseAuthentication();

app.MapGet("/", () => "Hello World!");


app.Run();
