using System.Security.Claims;
using AutoMapper;
using EduHub.Application.Interfaces;
using EduHub.Application.Models.Profile;
using EduHub.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.UI.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly ILogger<ProfileController> _logger;
    private readonly IMapper _mapper;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserService _userService;

    public ProfileController(IUserService userService, IMapper mapper, ILogger<ProfileController> logger,
        SignInManager<User> signInManager)
    {
        _mapper = mapper;
        _userService = userService;
        _logger = logger;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> MyProfile()
    {
        var myProfile =
            await _userService.GetUserProfileAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        return View(myProfile);
    }

    public async Task<IActionResult> EditProfile()
    {
        var user = await _userService.GetByIdAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));


        return View(_mapper.Map<EditProfileModel>(user));
    }

    public IActionResult ChangePassword()
    {
        return View();
    }

    public IActionResult BecomeTeacher()
    {
        return View();
    }

    public async Task<IActionResult> ChangePhoto()
    {
        var user = await _userService.GetByIdAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        var result = _mapper.Map<ChangePhotoModel>(user);
        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> EditProfile(EditProfileModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _userService.EditUserProfileAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                    model);
                TempData["success"] = "Edit profile successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
                TempData["error"] = $"Error: {ex.Message}";
                return View(model);
            }
        }
        else
        {
            TempData["error"] = "Validation failed.";
            return View(model);
        }

        return RedirectToAction("MyProfile");
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _userService.ChangePasswordAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                    model);
                TempData["success"] = "Change password successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
                TempData["error"] = $"Error: {ex.Message}";
                return View(model);
            }
        }
        else
        {
            TempData["error"] = "Validation failed.";
            return View(model);
        }

        return RedirectToAction("MyProfile");
    }

    [HttpPost]
    public async Task<IActionResult> ChangePhoto(ChangePhotoModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _userService.ChangePhotoAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                    model);
                TempData["success"] = "Change photo successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
                TempData["error"] = $"Error: {ex.Message}";

                return View(model);
            }
        }
        else
        {
            TempData["error"] = "Validation failed.";
            return View(model);
        }

        return RedirectToAction("MyProfile");
    }

    [HttpPost]
    public async Task<IActionResult> BecomeTeacher(TeacherRequestModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _userService.BecomeTeacher(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                    model);
                TempData["success"] = "Requested for teacher successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
                TempData["error"] = $"Error: {ex.Message}";
                return View(model);
            }
        }
        else
        {
            TempData["error"] = "Validation failed.";
            return View(model);
        }

        return RedirectToAction("MyProfile");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteAccount()
    {
        try
        {
            await _userService.DeleteAcountAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            TempData["success"] = "Account deleted successfully.";
        }
        catch (Exception ex)
        {
            TempData["error"] = $"Error: {ex.Message}";
            _logger.LogError(ex.Message);
        }

        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
}