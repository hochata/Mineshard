// Workaroud for https://github.com/dotnet/roslyn-analyzers/issues/6141
#pragma warning disable CA1852

using Mineshard.Api.Broker;

using Mineshard.Api.Mappings;
using Mineshard.Persistence.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add config
var parentDir = Directory.GetParent(Directory.GetCurrentDirectory());
builder.Configuration
    .SetBasePath(parentDir == null ? Directory.GetCurrentDirectory() : parentDir.FullName)
    .AddJsonFile("appsettings.json", optional: false);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inject services
builder.Services.AddScoped<IReportsRepo, ReportsDbRepo>();
builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
builder.Services.AddScoped<IRoleRepository, SqlRoleRepository>();
builder.Services.AddScoped<IProducer, Producer>();

// Add automapper configuration
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

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
