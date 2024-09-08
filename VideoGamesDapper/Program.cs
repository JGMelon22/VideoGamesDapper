using System.Data;
using System.Reflection;
using Npgsql;
using Serilog;
using VideoGamesDapper.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

# region [Database Registration]

builder.Services.AddScoped<IDbConnection>(x =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("Default"))
);

# endregion

# region [Repository Registration]

builder.Services.AddRepositories();

# endregion

# region [ MediatR Registration]

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

# endregion

# region [Validators Registration]

builder.Services.AddValidators();

# endregion

# region [ Serilog Registration ]

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

# endregion

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
