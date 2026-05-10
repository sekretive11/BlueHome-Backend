using BlueHome.Server.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Application.Spaces.Abstractions
{
    public interface ISpaceRepository
    {
        Task AddAsync(IoTSpace space, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(int spaceId, CancellationToken cancellationToken);
    }
}
