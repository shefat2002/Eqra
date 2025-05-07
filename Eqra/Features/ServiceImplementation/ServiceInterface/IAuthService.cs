using Eqra.Features.DomainModels;

namespace Eqra.Features.ServiceImplementation.ServiceInterface
{
    public interface IAuthService
    {
        Task<object> CreateUserAsync(RegisterDto request);
    }
}