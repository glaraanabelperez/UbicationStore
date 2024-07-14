

using Abrazos.Persistence.Database;
using AplicationCore.Interfaces;
using AplicationCore.Servicios;
using AplicationCore.Utils;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.IO;
using Serilog;
using Utils.Exception;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(builder.Configuration);

// DbContext

//builder.Services.AddDbContext<ApplicationDbContext>(
//    options => options.UseInMemoryDatabase(
//       builder.Configuration["ConnectionStrings:DefaultConnection"]
//        )
//    );

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseInMemoryDatabase("GeoDatabase")
//           .UseNetTopologySuite());

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseInMemoryDatabase(
       builder.Configuration["GeoDatabase"]
        )
    );

// Add Filters, tu handler Exceeptions with microsoft'libreries
builder.Services.AddMvc(option =>
{
    option.Filters.Add<ExceptionHandlerFilter>();
});

// Add services to the container.
builder.Services.AddTransient<StoreDbContext, ApplicationDbContext>();

builder.Services.AddTransient<IStoreService, StoreService>();

builder.Services.AddTransient<IResultApp, ResultApp>();

builder.Services.AddSingleton<GeoJsonWriter>();


//Cors
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
 builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
                          policy =>
                          {
                              policy.WithOrigins("*")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                          });
    });



builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
    context.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);


app.MapControllers();


app.Run();

