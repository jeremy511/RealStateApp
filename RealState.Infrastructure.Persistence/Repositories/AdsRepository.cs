

using RealState.Core.Application.Interfaces.Repositories;
using RealState.Infrastucture.Persistence.Contexts;
using RealState.Core.Domain.Entities;

namespace RealState.Infrastucture.Persistence.Repositories
{
    public class AdsRepository : GenericRepository<Ads>, IAdsRepository
    {
        private readonly ApplicationContext _DbContext;

        public AdsRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }
    }
}
