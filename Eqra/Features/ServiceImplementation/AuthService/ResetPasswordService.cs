using Eqra.Features.DataAccess.RepositoryInterface;
using Eqra.Features.DomainModels;
using Eqra.Features.ServiceImplementation.ServiceInterface;

namespace Eqra.Features.ServiceImplementation.AuthService
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public ResetPasswordService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public async Task<string> ResetPasswordAsync(ResetPasswordDto request)
        {
            //Not Implemented Yet!


            return "Password reset successfully.";
        }

    }
}
