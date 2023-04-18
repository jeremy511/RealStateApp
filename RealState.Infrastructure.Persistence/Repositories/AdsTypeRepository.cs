

using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Domain.Entities;
using RealState.Infrastructure.Persistence.Contexts;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class AdsTypeRepository : GenericRepository<AdsType>, IAdsTypeRepository
    {
        private readonly ApplicationContext _DbContext;

        public AdsTypeRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }
    }
}
