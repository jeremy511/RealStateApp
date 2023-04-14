
using RealState.Infrastucture.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using RealState.Core.Application.Interfaces.Repositories;

namespace RealState.Infrastucture.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly ApplicationContext _DbContext;

        public GenericRepository(ApplicationContext dbContext)
        {
            _DbContext = dbContext;
        }


        public virtual async Task<Entity> AddAsync(Entity entity)
        {
            await _DbContext.AddAsync(entity);
            await _DbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(Entity entity, int id)
        {
            var entry = await _DbContext.Set<Entity>().FindAsync(id);
            _DbContext.Entry(entry).CurrentValues.SetValues(entity);
            await _DbContext.SaveChangesAsync();
        }
        public virtual async Task DeleteAsync(Entity entity)
        {
            _DbContext.Set<Entity>().Remove(entity);
            await _DbContext.SaveChangesAsync();
        }

        public virtual async Task<List<Entity>> GetAllViewModel()
        {
            return await _DbContext.Set<Entity>().ToListAsync();
        }

        public virtual async Task<Entity> GetViewModelById(int id)
        {
            return await _DbContext.Set<Entity>().FindAsync(id);
        }

        public virtual async Task<List<Entity>> GetAllWithIncludes(List<string> properties)
        {
            var query = _DbContext.Set<Entity>().AsQueryable();

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }
    }
}
