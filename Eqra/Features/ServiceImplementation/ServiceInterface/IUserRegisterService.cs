using Eqra.Features.DomainModels;

namespace Eqra.Features.ServiceImplementation.ServiceInterface
{
    public interface IUserRegisterService
    {
        Task<object> CreateUserAsync(RegisterDto request);
    }
}