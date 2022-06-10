using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AlphaStellarWebApi.Data
{
    public class Repository<T> : IRepository<T> where T : Vehicle, new()
    {
        private readonly MyAppContext _myContext;
        public Repository(MyAppContext myContext)
        {
            _myContext = myContext;
        }
        public async Task<T> GetById(int id)
        {
            return await _myContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<int> DeleteCarById(int id)
        {
            var entity = await GetById(id);
            _myContext.Set<T>().Remove(entity);
            return await _myContext.SaveChangesAsync();
        }

        public async Task<int> UpdateField(T entity, Expression<Func<T, object>> proper)
        { 
            _myContext.Entry<T>(entity).Property(proper).IsModified = true;
            return await _myContext.SaveChangesAsync(); 
        }

        public async Task<T> Add(T entity)
        {
            await _myContext.Set<T>().AddAsync(entity);
            await _myContext.SaveChangesAsync();

            return entity;
        }

        public async Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var query = _myContext.Set<T>().AsQueryable();
            foreach (Expression<Func<T, object>> include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<List<T>> Filter(Expression<Func<T, bool>> expression)
        {
            var query = _myContext.Set<T>().AsQueryable();
            if (expression != null)
            {
                query = query.Where(expression);
            }

            return await query.ToListAsync();
        }
    }
}
