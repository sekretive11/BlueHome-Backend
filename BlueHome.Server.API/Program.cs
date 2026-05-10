using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Application.CommandHandlers;
using BlueHome.Server.Application.Devices.Commands;
using BlueHome.Server.Application.Locations.Commands;
using BlueHome.Server.Application.Spaces.Abstractions;
using BlueHome.Server.Application.Spaces.Commands;
using BlueHome.Server.Infrastructure.DependencyInjection;
using BlueHome.Server.Infrastructure.Persistence;
using BlueHome.Server.Infrastructure.Persistence.Repositories;
using BlueHome.Server.Infrastructure.Runtime;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();

builder.Services.AddScoped<TurnLampOnHandler>();
builder.Services.AddScoped<TurnLampOffHandler>();
builder.Services.AddScoped<SetLampBrightnessHandler>();
builder.Services.AddScoped<IDeviceRuntime, DeviceRuntime>();
builder.Services.AddScoped<ISpaceRepository, SpaceRepository>();
builder.Services.AddScoped<CreateSpaceCommandHandler>();
builder.Services.AddScoped<CreateLocationCommandHandler>();
builder.Services.AddScoped<RegisterDeviceCommandHandler>();

builder.Services.AddDbContext<BlueHomeDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();