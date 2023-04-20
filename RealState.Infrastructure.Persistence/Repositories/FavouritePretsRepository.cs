

using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Domain.Entities;
using RealState.Infrastructure.Persistence.Contexts;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class FavouritePretsRepository : GenericRepository<FavouriteProperties>, IFavouritePretsRepository
    {
        private readonly ApplicationContext _DbContext;

        public FavouritePretsRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _DbContext = dbContext;
        }
    }
}
