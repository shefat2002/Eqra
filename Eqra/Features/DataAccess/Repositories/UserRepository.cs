using System.Security.Cryptography;
using System.Text;
using Eqra.Features.DataAccess.DBContext;
using Eqra.Features.DataAccess.RepositoryInterface;
using Eqra.Features.DomainModels;
using Eqra.Features.ServiceImplementation.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplicationProduct.Features.DataAccess.Repositories;

namespace Eqra.Features.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
