using EduHub.Application.Models.Auth;
using EduHub.Application.ViewModels.Auth;

namespace EduHub.Application.Interfaces;

public interface IAuthService
{
    Task Login(LoginModel userModel);
    Task Register(RegisterModel userModel);
    Task ResetPasswordAsync(ResetPasswordModel model);
    Task VerifyUser(Guid? vc);
    Task SentResetPasswordToEmail(string email);
    Task<string> ResetForgotPassword(Guid? vc);
    Task ResetForgotPassword(ResetForgotPasswordModel model);

}