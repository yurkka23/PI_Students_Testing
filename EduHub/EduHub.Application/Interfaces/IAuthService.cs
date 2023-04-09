using Microsoft.AspNetCore.Identity;
using EduHub.Application.Interfaces;
using EduHub.Application.ViewModels.Auth;

namespace EduHub.Application.Interfaces;

public interface IAuthService
{
    Task Login(LoginModel userModel);
    Task Register(RegisterModel userModel);
    Task ResetPasswordAsync(ResetPasswordModel model);
}
