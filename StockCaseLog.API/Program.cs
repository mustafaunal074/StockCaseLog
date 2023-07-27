using Microsoft.EntityFrameworkCore;
using StockCaseLog.Repository.Abstract;
using StockCaseLog.Repository.Concreate;
using StockCaseLog.Repository.Context;
using StockCaseLog.Service.Abstract;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//builder.Services.AddScoped<DbContext, StockCaseLogDbContext>();
builder.Services.AddDbContext<StockCaseLogDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlConStr"].ToString(), o =>
    {
        o.MigrationsAssembly("StockCaseProject.Repository");
    });
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(IRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IStockService, IStockService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
