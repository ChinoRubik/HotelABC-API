using HotelABC_API.Data;
using HotelABC_API.Mappings;
using HotelABC_API.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// By default is builder.Services.AddControllers() but is added AddNewtonsoftJson() In order to update by patch=============
builder.Services.AddControllers().AddNewtonsoftJson();
//===========================================================================================================


//REGISTERING HTTPCONTEXTACCESSOR FOR IMAGES ==========================================
builder.Services.AddHttpContextAccessor();
// ================================================================


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// DB CONTEXT  =================================´
builder.Services.AddDbContext<HotelDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("HotelABCConnectionString")));
// ====================================================================


//REGISTERING REPOSITORIES ==========================================
builder.Services.AddScoped<IRoomRepository, SQLRoomRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
// ================================================================


//REGISTERING MAPPING ==========================================
builder.Services.AddAutoMapper(typeof(MainAutoMapper));
// ================================================================

// ============= REGISTEWRING IDENTITYUSER FOR LOGIN AND REGISTER ENDPOINTS =====================
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("HotelABC")
    .AddEntityFrameworkStores<HotelDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});
//=============================================================================


// REGISTER AUTHENTICATION FOR AUTHORIZATION ROUTES ========================
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });
//=================================================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// TO Authenticate settings
app.UseAuthentication(); 
app.UseAuthorization();

// Configuration to save IMAGES/STATIC FILES  =================================´
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});
// ====================================================================


app.MapControllers();

app.Run();
