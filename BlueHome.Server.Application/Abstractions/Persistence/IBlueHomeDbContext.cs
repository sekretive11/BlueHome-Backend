using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlueHome.Server.Domain.Entities;

namespace BlueHome.Server.Application.Abstractions.Persistence
{
    public interface IBlueHomeDbContext
    {
        DbSet<User> Users { get; }
        DbSet<IoTSpace> Spaces { get; }
        DbSet<Device> Devices { get; }
        DbSet<Location> Locations { get; }
        DbSet<EventLog> EventLogs { get; }
        DbSet<UserLog> UserLogs { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
