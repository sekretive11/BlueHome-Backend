using BlueHome.Server.Application.Abstractions.Persistence;
using BlueHome.Server.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueHome.Server.Domain.Entities;

namespace BlueHome.Server.Infrastructure.Persistence
{
    public class BlueHomeDbContext : DbContext, IBlueHomeDbContext
    {
        public BlueHomeDbContext(DbContextOptions<BlueHomeDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<IoTSpace> Spaces => Set<IoTSpace>();
        public DbSet<Device> Devices => Set<Device>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<EventLog> EventLogs => Set<EventLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // PostgreSQL enums
            modelBuilder.HasPostgresEnum<SpaceStatus>();
            modelBuilder.HasPostgresEnum<DeviceStatus>();

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(BlueHomeDbContext).Assembly
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
