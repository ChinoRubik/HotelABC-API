using HotelABC_API.Data;
using HotelABC_API.Mappings;
using HotelABC_API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// By default is builder.Services.AddControllers() but is added AddNewtonsoftJson() In order to update by patch
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
// ================================================================

//REGISTERING MAPPING ==========================================
builder.Services.AddAutoMapper(typeof(MainAutoMapper));
// ================================================================

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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
