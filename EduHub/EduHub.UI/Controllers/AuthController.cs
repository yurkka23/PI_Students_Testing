using EduHub.Application.Interfaces;
using EduHub.Application.Models.Auth;
using EduHub.Application.ViewModels.Auth;
using EduHub.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.UI.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;
    private readonly SignInManager<User> _signInManager;

    public AuthController(SignInManager<User> signInManager, IAuthService authService,
        ILogger<AuthController> logger)
    {
        _signInManager = signInManager;
        _authService = authService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _authService.Register(model);
                TempData["success"] = "Sign up successfully. Please before sign in confirm your email during 14 days!";

                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["error"] = $"Error: {ex.Message}";
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        return View(new LoginModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _authService.Login(model);
                TempData["success"] = "Login successfully";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
                TempData["error"] = $"Error: {ex.Message}";
            }
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        TempData["success"] = "Logout successfully";

        return RedirectToAction("Index", "Home");
    }

    [AllowAnonymous]
    public async Task<IActionResult> VerifyUser([FromQuery] Guid? vc)
    {
        try
        {
            await _authService.VerifyUser(vc);
            TempData["success"] = "Verified email successfully";
            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            TempData["error"] = $"Error: {ex.Message}";
            return RedirectToAction("Register", "Auth");
        }
    }
    public IActionResult ForgotPassword()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> ForgotPasswordLink(EmailModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _authService.SentResetPasswordToEmail(model.Email);
                TempData["success"] = "Link sent successfully. Please check email and reset password";
                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
                TempData["error"] = $"Error: {ex.Message}";
            }
        }

        return View(model);
    }

    [AllowAnonymous]
    public async Task<IActionResult> ResetForgotPassword([FromQuery] Guid? vc)
    {
        try
        {
            var email = await _authService.ResetForgotPassword(vc);
            var model = new ResetForgotPasswordModel
            {
                Email = email,
            };
            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            TempData["error"] = $"Error: {ex.Message}";
            return RedirectToAction("Login", "Auth");
        }
    }

    [HttpPost]
    public async Task<IActionResult> ResetForgotPassword(ResetForgotPasswordModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _authService.ResetForgotPassword(model);
                TempData["success"] = "Password reset successfully. Please sign in";
                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
                TempData["error"] = $"Error: {ex.Message}";
            }
        }

        return View(model);
    }
}