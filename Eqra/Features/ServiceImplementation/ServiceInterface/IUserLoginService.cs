using Eqra.Features.DomainModels;

namespace Eqra.Features.ServiceImplementation.ServiceInterface
{
    public interface IUserLoginService
    {
        Task<string> LoginUserAsync(LoginDto request);
    }
}