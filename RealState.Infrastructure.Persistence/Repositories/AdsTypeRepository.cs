

using RealState.Core.Application.Interfaces.Repositories;
using RealState.Infrastucture.Persistence.Contexts;
using RealState.Core.Domain.Entities;

namespace RealState.Infrastucture.Persistence.Repositories
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
