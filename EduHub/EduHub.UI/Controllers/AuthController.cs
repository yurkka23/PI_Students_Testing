using EduHub.Application.Interfaces;
using EduHub.Application.ViewModels.Auth;
using EduHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.UI.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(SignInManager<User> signInManager, IAuthService authService, ILogger<AuthController> logger)
    {
        this._signInManager = signInManager;
        this._authService = authService;
        this._logger = logger;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (this.ModelState.IsValid)
        {
            try
            {
                await this._authService.Register(model);
                TempData["success"] = "Sign up successfully. Please sign in";

                return this.RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                TempData["error"] = $"Error: {ex.Message}";
                this.ModelState.AddModelError(string.Empty, ex.Message);
            }
        }

        return this.View(model);
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        return this.View(new LoginModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (this.ModelState.IsValid)
        {
            try
            {
                await this._authService.Login(model);
                TempData["success"] = "Login successfully";
                return this.RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                this.ModelState.AddModelError(string.Empty, ex.Message);
                TempData["error"] = $"Error: {ex.Message}";
            }
        }

        return this.View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await this._signInManager.SignOutAsync();
        TempData["success"] = "Logout successfully";

        return this.RedirectToAction("Index", "Home");
    }

}
