

using RealState.Core.Application.Interfaces.Repositories;
using RealState.Infrastucture.Persistence.Contexts;
using RealState.Core.Domain.Entities;

namespace RealState.Infrastucture.Persistence.Repositories
{
    public class SaleRepository : GenericRepository<Sales>, ISaleRepository
    {
        private readonly ApplicationContext _DbContext;

        public SaleRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }
    }
}
