

using RealState.Core.Application.Interfaces.Repositories;
using RealState.Infrastucture.Persistence.Contexts;
using RealState.Core.Domain.Entities;

namespace RealState.Infrastucture.Persistence.Repositories
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
