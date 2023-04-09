using EduHub.Application.Interfaces;
using EduHub.Application.ViewModels.Auth;
using EduHub.Domain.Constants;
using EduHub.Domain.Entities;
using EduHub.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace EduHub.Application.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;
    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<AppRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task Login(LoginModel userModel)
    {
        var user = await _userManager.FindByEmailAsync(userModel.Email);

        if (user is null)
        {
            throw new BadRequestException("Incorrect login");
        }

        var result = await _signInManager.PasswordSignInAsync(user, userModel.Password, false, false);

        if (!result.Succeeded)
        {
            throw new BadRequestException("Incorrect password");
        }

    }
    public async Task Register(RegisterModel userModel)
    {
        var checkUserEmail = await _userManager.FindByEmailAsync(userModel.Email);
        if (checkUserEmail != null) throw new BadRequestException("User with such email already exists");
        var checkUserName = await _userManager.FindByNameAsync(userModel.UserName);
        if (checkUserName != null) throw new BadRequestException("User with such username already exists");

        var user = new User
        {
            UserName = userModel.UserName,
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            Email = userModel.Email,
            RegisterTime = DateTimeOffset.Now,
            UserImgUrl = null
        };

        var result = await _userManager.CreateAsync(user, userModel.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);

            throw new BadRequestException(string.Join(", ", errors));

        }

       var result1 = await _userManager.AddToRoleAsync(user, RoleConstants.StudentRole);

        if (!result1.Succeeded)
        {
            var errors = result1.Errors.Select(e => e.Description);

            throw new BadRequestException(string.Join(", ", errors));
        }
    }

    public async Task ResetPasswordAsync(ResetPasswordModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
            throw new BadRequestException("Invalid Request");

        var resetPassResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);

        if (!resetPassResult.Succeeded)
        {
            var errors = resetPassResult.Errors.Select(e => e.Description);

            throw new BadRequestException(string.Join(", ", errors));
        }

    }
}
