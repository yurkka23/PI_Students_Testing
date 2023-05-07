using System.Security.Claims;
using AutoMapper;
using EduHub.Application.Interfaces;
using EduHub.Application.Models.Course;
using EduHub.Domain.Constants;
using EduHub.UI.ViewModels;
using EduHub.UI.ViewModels.Teacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.UI.Controllers;

public class CourseController : Controller
{
    private readonly ICourseService _courseService;
    private readonly ILogger<CourseController> _logger;
    private readonly IMapper _mapper;

    public CourseController(ICourseService courseService, IMapper mapper, ILogger<CourseController> logger)
    {
        _mapper = mapper;
        _courseService = courseService;
        _logger = logger;
    }

    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> TeachersCourses()
    {
        var courses =
            await _courseService.GetTeachersCoursesAsync(
                Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        return View(courses);
    }

    [HttpGet]
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> AddCourse()
    {
        var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
        var vm = new CreateCourseVM()
        {
            Courses = courses
        };

        return View(vm);
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> AddCourse(CreateCourseVM model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var courseId = await _courseService.CreateCourseAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                    model.CreateCourse);
                TempData["success"] = "Course added!";

                return RedirectToAction("Course", "Course", new { id = courseId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["error"] = $"Error: {ex.Message}";
                ModelState.AddModelError(string.Empty, ex.Message);
            }
        }
        var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        model.Courses = courses;

        return View(model);
    }

    [HttpGet]
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]

    public async Task<IActionResult> Course(Guid id)
    {
        var course = await _courseService.GetCourseAsync(id);

        var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        var vm = new CourseVM()
        {
            Course = course,
            Courses = courses
        };

        return this.View(vm);
    }

    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> EditCourse(Guid id)
    {
        var course = await _courseService.GetCourseAsync(id);
        var editcourse = _mapper.Map<EditCourseModel>(course);
        var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        var vm = new EditCourseVM
        {
            EditCourse = editcourse,
            Courses = courses
        };

        return View(vm);
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> EditCourse(EditCourseVM model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _courseService.EditCourseAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                    model.EditCourse);
                TempData["success"] = "Edit course successfully.";
            }
            catch (Exception ex)
            {
                var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

                model.Courses = courses;

                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
                TempData["error"] = $"Error: {ex.Message}";
                return View(model);
            }
        }
        else
        {
            var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

            model.Courses = courses;
            TempData["error"] = "Validation failed.";
            return View(model);
        }

        return RedirectToAction("Course", "Course", new { id = model.EditCourse.Id });
    }

    [HttpGet]
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> DeleteCourse(Guid id)
    {
        try
        {
            await _courseService.DeleteCourseAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), id);
            TempData["success"] = "Course deleted successfully.";
        }
        catch (Exception ex)
        {
            TempData["error"] = $"Error: {ex.Message}";
            _logger.LogError(ex.Message);
        }

        return RedirectToAction("TeachersCourses", "Course");
    }

    [Authorize]
    public async Task<IActionResult> AddStudentToCourse([FromQuery] string password , Guid courseId)
    {
        try
        {
            await _courseService.EnrollToCourseWithPassword(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), courseId, password);
            TempData["success"] = "You enrolled to course.";
        }
        catch (Exception ex)
        {
            TempData["error"] = $"Error: {ex.Message}";
            _logger.LogError(ex.Message);
            return RedirectToAction("Search", "Home");
        }

        return RedirectToAction("StudentCourse", "Course", new {id = courseId});


    }
    [Authorize]
    public async Task<IActionResult> AddCourseToStudentWithoutPassword([FromQuery] Guid courseId)
    {
        try
        {
            await _courseService.EnrollToCourseWithOutPassword(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), courseId);
            TempData["success"] = "You enrolled to course.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            TempData["error"] = $"Error: {ex.Message}";
            return RedirectToAction("Search", "Home");

        }
        return RedirectToAction("StudentCourse", "Course", new { id = courseId });
    }

    [Authorize]
    public async Task<IActionResult> StudentCourses()
    {
        var courses =
            await _courseService.GetStudentsCoursesAsync(
                Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        return View(courses);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> StudentCourse(Guid id)
    {
        var course = await _courseService.GetCourseAsync(id);

        var courses = await _courseService.GetStudentsCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        var vm = new CourseVM()
        {
            Course = course,
            Courses = courses
        };

        return this.View(vm);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> LeaveCourse(Guid id)
    {
        try
        {
            await _courseService.LeaveCourseAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), id);
            TempData["success"] = "You left course.";
        }
        catch (Exception ex)
        {
            TempData["error"] = $"Error: {ex.Message}";
            _logger.LogError(ex.Message);
        }

        return RedirectToAction("StudentCourses", "Course");
    }


}
