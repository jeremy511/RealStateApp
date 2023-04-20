

using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Domain.Entities;
using RealState.Infrastructure.Persistence.Contexts;

namespace RealState.Infrastructure.Persistence.Repositories
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
