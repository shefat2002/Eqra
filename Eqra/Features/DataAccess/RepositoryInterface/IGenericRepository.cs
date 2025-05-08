
using System.Linq.Expressions;
using Eqra.Features.DomainModels;

namespace Eqra.Features.DataAccess.RepositoryInterface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> FindAllAsync();
        Task AddAsync(T entity);
        Task Delete(T entity);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] Includes);
        Task Update(T entity);
    }
}