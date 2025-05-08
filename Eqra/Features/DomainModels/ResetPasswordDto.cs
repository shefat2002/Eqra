namespace Eqra.Features.DomainModels
{
    public class ResetPasswordDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
