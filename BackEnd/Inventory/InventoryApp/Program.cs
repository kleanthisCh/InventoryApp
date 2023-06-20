using InventoryApp.Configuration;
using InventoryApp.DAO;
using InventoryApp.Data;
using InventoryApp.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("InventoryDBConnection");
builder.Services.AddDbContext<InventoryContext>(options => {
    options.UseSqlServer(connString);
    options.EnableSensitiveDataLogging( true);
    });
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors( options => options.AddPolicy("AllowAll", policy => policy
                                                        .AllowAnyOrigin()
                                                        .AllowAnyMethod()
                                                        .AllowAnyHeader()
                                                        ));

builder.Services.AddScoped<IManufacturerDAO,ManufacturerDAOImpl>();
builder.Services.AddScoped<IManufacturerService,ManufacturerServiceImpl>();

builder.Services.AddScoped<ITypeDAO, TypeDAOImpl>();
builder.Services.AddScoped<ITypeService, TypeServiceImpl>();

builder.Services.AddScoped<IGenderDAO, GenderDAOImpl>();
builder.Services.AddScoped<IGenderService, GenderServiceImpl>();

builder.Services.AddScoped<IProductDAO, ProductDAOImpl>();
builder.Services.AddScoped<IProductService, ProductServiceImpl>();

builder.Services.AddScoped<IBarcodeDAO, BarcodeDAOImpl>();
builder.Services.AddScoped<IBarcodeService, BarcodeServiceImpl>();

builder.Services.AddAutoMapper(typeof(MapperConfig));

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
app.UseCors("AllowAll");
app.Run();
