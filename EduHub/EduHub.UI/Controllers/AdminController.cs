using System.Security.Claims;
using AutoMapper;
using EduHub.Application.DTOs.User;
using EduHub.Application.Interfaces;
using EduHub.Application.Models.PaginatedList;
using EduHub.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.UI.Controllers;

[Authorize(Roles = RoleConstants.AdminRole)]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;
    private readonly ILogger<AdminController> _logger;
    private readonly IMapper _mapper;
    private readonly int _sizeLimit = 2;
    private readonly IUserService _userService;

    public AdminController(IAdminService adminService, IMapper mapper, ILogger<AdminController> logger,
        IUserService userService)
    {
        _mapper = mapper;
        _adminService = adminService;
        _logger = logger;
        _userService = userService;
    }

    public async Task<IActionResult> TeachersRequests()
    {
        var requests = await _adminService.GetTeachersRequestsAsync();

        return View(requests);
    }

    public async Task<IActionResult> Teachers(string search, int pageNum = 1)
    {
        ViewData["CurrentFilter"] = search;
        pageNum = pageNum == 0 ? 1 : pageNum;

        var teachers = await _adminService.GetTeachersAsync(pageNum, _sizeLimit, search);

        var paginatedListModel =
            new PaginatedListModel<UserDTO>(teachers.teachers.ToList(), teachers.count, pageNum, _sizeLimit);

        return View(paginatedListModel);
    }

    public async Task<IActionResult> Students(string search, int pageNum = 1)
    {
        ViewData["CurrentFilter"] = search;
        pageNum = pageNum == 0 ? 1 : pageNum;

        var students = await _adminService.GetStudentsAsync(pageNum, _sizeLimit, search);

        var paginatedListModel =
            new PaginatedListModel<UserDTO>(students.students.ToList(), students.count, pageNum, _sizeLimit);

        return View(paginatedListModel);
    }

    [HttpGet]
    public async Task<IActionResult> AddTeacher(Guid id)
    {
        try
        {
            await _adminService.AddTeacherAsync(id, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            TempData["success"] = "Add Teacher successfully.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            TempData["error"] = $"Error: {ex.Message}";
        }

        return RedirectToAction("TeachersRequests");
    }

    [HttpGet]
    public async Task<IActionResult> DenyTeacher(Guid id)
    {
        try
        {
            await _adminService.DenyTeacherAsync(id, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            TempData["success"] = "Deny Teacher successfully.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            TempData["error"] = $"Error: {ex.Message}";
        }

        return RedirectToAction("TeachersRequests");
    }

    [HttpGet]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        try
        {
            await _userService.DeleteAcountAsync(id);
            TempData["success"] = "Account deleted successfully.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            TempData["error"] = $"Error: {ex.Message}";
        }

        return RedirectToAction("Students");
    }

    [HttpGet]
    public async Task<IActionResult> RemoveFromTeachers(Guid id)
    {
        try
        {
            await _adminService.RemoveFromTeacherAsync(id);
            TempData["success"] = "Teacher removed successfully.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            TempData["error"] = $"Error: {ex.Message}";
        }

        return RedirectToAction("Teachers");
    }
    public async Task<IActionResult> StudentProfile(Guid id)
    {
        var res = await _userService.GetStudentProfileAsync(id);

        return this.View(res);
    }
}