using Eqra.Features.DataAccess.RepositoryInterface;
using Eqra.Features.DomainModels;
using Eqra.Features.ServiceImplementation.ServiceInterface;

namespace Eqra.Features.ServiceImplementation.AuthService
{
    public class ForgetPasswordService : IForgetPasswordService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public ForgetPasswordService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<string> ForgetPasswordAsync(ForgetPasswordDto request)
        {
            //Not Implemented Yet!


            return "Reset password link has been sent to your email.";
        }

    }
}
