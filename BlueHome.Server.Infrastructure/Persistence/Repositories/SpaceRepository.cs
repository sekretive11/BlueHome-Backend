using BlueHome.Server.Application.Spaces.Abstractions;
using BlueHome.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueHome.Server.Infrastructure.Persistence.Repositories
{
    public sealed class SpaceRepository : ISpaceRepository
    {
        private readonly BlueHomeDbContext _dbContext;

        public SpaceRepository(BlueHomeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(IoTSpace space, CancellationToken cancellationToken)
        {
            _dbContext.Spaces.Add(space);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(int spaceId, CancellationToken cancellationToken)
        {
            return await _dbContext.Spaces
                .AnyAsync(x => x.SpaceId == spaceId, cancellationToken);
        }
    }
}
