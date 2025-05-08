using Eqra.Features.DomainModels;

namespace Eqra.Features.DataAccess.RepositoryInterface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<bool> IsUserExist(string username);
    }
}