using System.Reflection;
using MediatR;
using Wallet.API.Filters;
using Wallet.Infrastructure;
using Wallet.Infrastructure.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(opt => opt.Filters.Add(typeof(ApiGlobalExceptionFilter)));
builder.Services.Configure<RouteOptions>(opt => { opt.LowercaseUrls = true; });

builder.Services
    .ConfigureApplication()
    .ConfigureInfrastructure(builder.Configuration);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

CreateDatabase(app);

// app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();


static void CreateDatabase(WebApplication app)
{
    var serviceScope = app.Services.CreateScope();
    var dataContext = serviceScope.ServiceProvider.GetService<WalletDbContext>();
    dataContext?.Database.EnsureCreated();
}