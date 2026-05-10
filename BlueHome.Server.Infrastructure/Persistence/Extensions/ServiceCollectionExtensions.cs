using BlueHome.Server.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Persistence.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPostgresPersistence(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<BlueHomeDbContext>(options =>
                options.UseNpgsql(connectionString)
            );

            services.AddScoped<IBlueHomeDbContext>(
                provider => provider.GetRequiredService<BlueHomeDbContext>());

            return services;
        }
    }
}
