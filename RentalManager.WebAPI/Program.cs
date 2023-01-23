using Microsoft.EntityFrameworkCore;
using RentalManager.Core.Repositories;
using RentalManager.Infrastructure.Repositories;
using RentalManager.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
var policyName = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
                      builder =>
                      {
                          builder
                            .WithOrigins("http://192.168.1.46:3000", "https://192.168.1.46:3000", "http://localhost:3000")
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .AllowAnyHeader();
                      });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IRentalEquipmentRepository, RentalEquipmentRepository>();
builder.Services.AddScoped<IRentalEquipmentService, RentalEquipmentService>();

builder.Services.AddScoped<IRentalAgreementRepository, RentalAgreementRepository>();
builder.Services.AddScoped<IRentalAgreementService, RentalAgreementService>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

//builder.Services.AddDbContext<AppDbContext>(
//    options => options.UseSqlServer("Server=system-monitor-db;Initial Catalog=systemMonitor;User=sa;Password=2620dvxje!ABC;TrustServerCertificate=True")
//);

builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("InMemoryDB"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory" && context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();