

using RealState.Core.Application.Interfaces.Repositories;
using RealState.Infrastucture.Persistence.Contexts;
using RealState.Core.Domain.Entities;

namespace RealState.Infrastucture.Persistence.Repositories
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
