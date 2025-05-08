using Eqra.Features.DomainModels;

namespace Eqra.Features.ServiceImplementation.ServiceInterface
{
    public interface IForgetPasswordService
    {
        Task<string> ForgetPasswordAsync(ForgetPasswordDto request);
    }
}