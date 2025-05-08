using Eqra.Features.DomainModels;

namespace Eqra.Features.ServiceImplementation.ServiceInterface
{
    public interface IResetPasswordService
    {
        Task<string> ResetPasswordAsync(ResetPasswordDto request);
    }
}