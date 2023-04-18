

using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Domain.Entities;
using RealState.Infrastructure.Persistence.Contexts;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class AdsUpgrateRepository : GenericRepository<AdsUpgrates>, IAdsUpgrateRepository
    {
        private readonly ApplicationContext _DbContext;

        public AdsUpgrateRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }
    }
}
