using Autofac;
using Autofac.Extensions.DependencyInjection;
using QuickBuy.Database.DbContext;
using QuickBuy.WEB.Autofac;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using QuickBuy.Automapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:54803") 
                     .AllowAnyMethod()
                     .AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<QuickBuyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacModule());
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<QuickBuyDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
