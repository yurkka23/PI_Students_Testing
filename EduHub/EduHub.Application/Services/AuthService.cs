using EduHub.Application.Interfaces;
using EduHub.Application.Models.Auth;
using EduHub.Application.ViewModels.Auth;
using EduHub.Domain.Constants;
using EduHub.Domain.Entities;
using EduHub.Domain.Exceptions;
using EduHub.Domain.Settings;
using EduHub.Integration.Providers.Abstractions;
using EduHub.Persistence.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Web;

namespace EduHub.Application.Services;

public class AuthService : IAuthService
{
    private readonly IEmailProvider _emailProvider;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UrlSettings _urlSettings;
    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager,
        RoleManager<AppRole> roleManager, IEmailProvider emailProvider, IUnitOfWork unitOfWork, UrlSettings urlSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _emailProvider = emailProvider;
        _unitOfWork = unitOfWork;
        _urlSettings = urlSettings;
    }

    public async Task Login(LoginModel userModel)
    {
        var user = await _userManager.FindByEmailAsync(userModel.Email);

        if (user is null)
        {
            throw new BadRequestException("Incorrect login");
        }

        if (!user.EmailConfirmed)
        {
            throw new BadRequestException("Please, confirm your email");
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
        if (checkUserEmail != null)
        {
            throw new BadRequestException("User with such email already exists");
        }

        var checkUserName = await _userManager.FindByNameAsync(userModel.UserName);
        if (checkUserName != null)
        {
            throw new BadRequestException("User with such username already exists");
        }

        var user = new User
        {
            UserName = userModel.UserName,
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            Email = userModel.Email,
            RegisterTime = DateTimeOffset.Now,
            UserImgUrl = null
        };

        var verificationCode = Guid.NewGuid();

        user.VerificationCode = verificationCode;
        user.VerificationExpires = DateTimeOffset.UtcNow.AddDays(14);

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

        var uriBuilder = new UriBuilder(_urlSettings.VerifyUserUrl);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        query["vc"] = verificationCode.ToString();
        uriBuilder.Query = query.ToString();

        await _emailProvider.SendEmailAsync(
            user.Email,
            "Verify your email address",
            $"<h2>Hello {user.FirstName}!</h2> <br>  <a href=\"{uriBuilder.ToString()}\">Verify email address</a>",
            true,
            true
        );
    }

    public async Task ResetPasswordAsync(ResetPasswordModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            throw new BadRequestException("Invalid Request");
        }

        var resetPassResult = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.Password);

        if (!resetPassResult.Succeeded)
        {
            var errors = resetPassResult.Errors.Select(e => e.Description);

            throw new BadRequestException(string.Join(", ", errors));
        }
    }
    public async Task VerifyUser(Guid? vc)
    {
        var user = await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.VerificationCode == vc);
        if (user == null)
        {
            throw new BadRequestException("User with such verification code doesn't found. ");
        }
        if(user.VerificationExpires <= DateTimeOffset.Now)
        {
            _unitOfWork.Users.Delete(user);
            var result = await _unitOfWork.SaveAsync(user.Id);
            throw new BadRequestException("Your varification expired so your account was deleted. Please register again and verify your email during 14 days!");
        }

        user.EmailConfirmed = true;

        user.VerificationExpires = null;
        user.VerificationCode = null;

        _unitOfWork.Users.Update(user);
        var res = await _unitOfWork.SaveAsync(user.Id);

    }

    public async Task SentResetPasswordToEmail(string email)
    {
        var user = await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
        {
            throw new BadRequestException("User with such email doesn't found. ");
        }
        var verificationCode = Guid.NewGuid();

        user.VerificationCode = verificationCode;

        _unitOfWork.Users.Update(user);
        var res = await _unitOfWork.SaveAsync(user.Id);

        var uriBuilder = new UriBuilder(_urlSettings.ResetPasswordUrl);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        query["vc"] = verificationCode.ToString();
        uriBuilder.Query = query.ToString();

        await _emailProvider.SendEmailAsync(
            user.Email,
            "Reset forgot password",
            $"<h2>Hello {user.FirstName}!</h2> <br> <p>You sent request for reset forgot password </p> <br> <a href=\"{uriBuilder.ToString()}\">Reset forgot password</a>",
            true,
            true
        );

    }

    public async Task<string> ResetForgotPassword(Guid? vc)
    {
        var user = await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.VerificationCode == vc);
        if (user == null)
        {
            throw new BadRequestException("User with such verification code doesn't found. ");
        }
        return user.Email;
    }
    public async Task ResetForgotPassword(ResetForgotPasswordModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            throw new BadRequestException("User doesn't exists with such email.");
        }

        string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        var resetPassResult = await _userManager.ResetPasswordAsync(user, resetToken, model.Password);

        if (!resetPassResult.Succeeded)
        {
            var errors = resetPassResult.Errors.Select(e => e.Description);

            throw new BadRequestException(string.Join(", ", errors));
        }
        user.VerificationCode = null;
        await _userManager.UpdateAsync(user);

    }

}