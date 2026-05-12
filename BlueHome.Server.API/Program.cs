using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Application.Abstractions.Auth;
using BlueHome.Server.Application.Auth.Commands;
using BlueHome.Server.Application.CommandHandlers;
using BlueHome.Server.Application.Devices.Commands;
using BlueHome.Server.Application.Events;
using BlueHome.Server.Application.Locations.Commands;
using BlueHome.Server.Application.Spaces.Abstractions;
using BlueHome.Server.Application.Spaces.Commands;
using BlueHome.Server.Domain.Events;
using BlueHome.Server.Infrastructure.Auth;
using BlueHome.Server.Infrastructure.DependencyInjection;
using BlueHome.Server.Infrastructure.Events;
using BlueHome.Server.Infrastructure.Events.Handlers;
using BlueHome.Server.Infrastructure.Persistence;
using BlueHome.Server.Infrastructure.Persistence.Repositories;
using BlueHome.Server.Infrastructure.Runtime;
using BlueHome.Server.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
builder.Services.AddScoped<IDomainEventHandler<DevicePoweredOnEvent>, BluetoothDeviceEventHandler>();
builder.Services.AddScoped<IDomainEventHandler<DevicePoweredOffEvent>, BluetoothDeviceEventHandler>();
builder.Services.AddScoped<IDomainEventHandler<DeviceBrightnessChangedEvent>, BluetoothDeviceEventHandler>();
builder.Services.AddScoped<IDomainEventHandler<DevicePoweredOnEvent>, EventLogHandler>();
builder.Services.AddScoped<IDomainEventHandler<DevicePoweredOffEvent>, EventLogHandler>();
builder.Services.AddScoped<IDomainEventHandler<DeviceBrightnessChangedEvent>, EventLogHandler>();
builder.Services.AddScoped<MoveDeviceHandler>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
builder.Services.AddScoped<IEventHandler<DevicePoweredOnEvent>, DevicePoweredOnAuditHandler>();
builder.Services.AddScoped<IEventHandler<DevicePoweredOffEvent>, DevicePoweredOffAuditHandler>();
builder.Services.AddScoped<IEventHandler<DeviceBrightnessChangedEvent>, DeviceBrightnessChangedAuditHandler>();
builder.Services.AddScoped<IEventHandler<DeviceMovedEvent>, DeviceMovedAuditHandler>();
builder.Services.AddScoped<ISpaceAccessService, SpaceAccessService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BlueHome.Server.API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "¬ведите JWT токен в формате: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var jwtSection = builder.Configuration.GetSection("Jwt");
var jwt = jwtSection.Get<JwtSettings>()
    ?? throw new InvalidOperationException("Jwt config is missing");

builder.Services.AddSingleton(jwt!);
builder.Services.AddScoped<LoginHandler>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwt!.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwt.Secret))
        };
    });

builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<BlueHomeDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();