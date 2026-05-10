using BlueHome.Server.Application.Abstractions;
using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Infrastructure.Bluetooth;
using BlueHome.Server.Infrastructure.Bluetooth.Emulation;
using BlueHome.Server.Infrastructure.Persistence;
using BlueHome.Server.Infrastructure.Runtime;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.DependencyInjection
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IBlueHomeDbContext, BlueHomeDbContext>();

            services.AddSingleton<DeviceSessionCache>();
            services.AddScoped<DeviceSessionFactory>();
            services.AddSingleton<IDeviceRuntime, DeviceRuntime>();

            services.AddSingleton<IBluetoothGateway, LampBluetoothEmulator>();
            services.AddSingleton<IEventPublisher, BluetoothEventPublisher>();

            return services;
        }
    }
}
