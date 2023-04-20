

using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Domain.Entities;
using RealState.Infrastructure.Persistence.Contexts;

namespace RealState.Infrastructure.Persistence.Repositories
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
