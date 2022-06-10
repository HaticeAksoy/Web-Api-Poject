using Domain.Entity;
using System.Linq.Expressions;

namespace AlphaStellarWebApi.Data
{
    public interface IRepository<T> where T : Vehicle
    {
       
        Task<T> Add(T entity);
        Task<T> GetById(int id);       
        Task<List<T>> GetAll(params Expression<Func<T, object>>[] includes);
        Task<List<T>> Filter(Expression<Func<T, bool>> expression);
        
        Task<int> UpdateField(T entity, Expression<Func<T,object>> proper);
        Task<int> DeleteCarById(int id);
    }
}
