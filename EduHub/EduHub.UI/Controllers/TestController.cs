using AutoMapper;
using EduHub.Application.Interfaces;
using EduHub.Application.Models.Question;
using EduHub.Application.Models.Test;
using EduHub.Domain.Constants;
using EduHub.UI.ViewModels.Answer;
using EduHub.UI.ViewModels.Question;
using EduHub.UI.ViewModels.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EduHub.UI.Controllers;

public class TestController : Controller
{
    private readonly ITestService _testService;
    private readonly ICourseService _courseService;
    private readonly ILogger<TestController> _logger;
    private readonly IMapper _mapper;

    public TestController(ITestService testService, IMapper mapper, ILogger<TestController> logger, ICourseService courseService)
    {
        _mapper = mapper;
        _testService = testService;
        _logger = logger;
        _courseService = courseService;
    }

    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> AddTest(Guid id)
    {
        var course = await _courseService.GetCourseAsync(id);
        var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        var vm = new AddTestVM
        {
            Course = course,
            Courses = courses
        };

        return View(vm);
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> AddTest(AddTestVM model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _testService.CreateTestAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                    model.AddTest.CourseId, model.AddTest);
                TempData["success"] = "Test added successfully.";
            }
            catch (Exception ex)
            {
                var course = await _courseService.GetCourseAsync(model.AddTest.CourseId);
                var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
                model.Courses = courses;
                model.Course = course;

                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
                TempData["error"] = $"Error: {ex.Message}";
                return View(model);
            }
        }
        else
        {
            var course = await _courseService.GetCourseAsync(model.AddTest.CourseId);
            var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            model.Courses = courses;
            model.Course = course;

            TempData["error"] = "Validation failed.";
            return View(model);
        }

        return RedirectToAction("Course", "Course", new { id = model.AddTest.CourseId });
    }

    [HttpGet]
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> DeleteTest(Guid id, Guid courseId)
    {
        try
        {
            await _testService.DeleteTestAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), id);
            TempData["success"] = "Test deleted successfully.";
        }
        catch (Exception ex)
        {
            TempData["error"] = $"Error: {ex.Message}";
            _logger.LogError(ex.Message);
        }

        return RedirectToAction("Course", "Course", new { id = courseId });
    }

    [HttpGet]
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]

    public async Task<IActionResult> Test(Guid id)
    {
        var test = await _testService.GetTestAsync(id);

        var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        var vm = new TestVM()
        {
            Test = test,
            Courses = courses
        };

        return this.View(vm);
    }

    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> EditTest(Guid id)
    {
        var test = await _testService.GetTestAsync(id);
        var edittest = _mapper.Map<EditTestModel>(test);
        var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        var vm = new EditTestVM
        {
            EditTest = edittest,
            Courses = courses
        };

        return View(vm);
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> EditTest(EditTestVM model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _testService.EditTestAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                    model.EditTest);
                TempData["success"] = "Edit test successfully.";
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

        return RedirectToAction("Test", "Test", new { id = model.EditTest.Id });
    }
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> TestResults(Guid testId)
    {
        var test = await _testService.GetTestAsync(testId);

        var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
        var testResults = await _testService.GetTestResultsForTeacherAsync(testId);

        var vm = new TestVM()
        {
            Test = test,
            Courses = courses,
            Results = testResults
        };

        return this.View(vm);
    }


    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> AddQuestion(Guid id)
    {
        var test = await _testService.GetTestAsync(id);
        var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        var vm = new AddQuestionVM
        {
            Test = test,
            Courses = courses
        };

        return View(vm);
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> AddQuestion(AddQuestionVM model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _testService.CreateQuestionAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), model.AddQuestion);
                TempData["success"] = "Question added successfully.";
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

        return RedirectToAction("Test", "Test", new { id = model.AddQuestion.TestId });
    }

    [HttpGet]
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> DeleteQuestion(Guid id, Guid testId)
    {
        try
        {
            await _testService.DeleteQuestionAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), id);
            TempData["success"] = "Question deleted successfully.";
        }
        catch (Exception ex)
        {
            TempData["error"] = $"Error: {ex.Message}";
            _logger.LogError(ex.Message);
        }

        return RedirectToAction("Test", "Test", new { id = testId });
    }

    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> EditQuestion(Guid id)
    {
        var question = await _testService.GetQuestionAsync(id);
        var editQuestion = _mapper.Map<EditQuestionModel>(question);
        var test = await _testService.GetTestAsync(question.TestId);
        var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        var vm = new EditQuestionVM
        {
            EditQuestion = editQuestion,
            Test = test,
            Courses = courses
        };

        return View(vm);
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> EditQuestion(EditQuestionVM model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _testService.EditQuestionAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                    model.EditQuestion);
                TempData["success"] = "Edit test successfully.";
            }
            catch (Exception ex)
            {
                var test = await _testService.GetTestAsync(model.EditQuestion.TestId);
                var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

                model.Test = test;
                model.Courses = courses;

                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
                TempData["error"] = $"Error: {ex.Message}";

                return View(model);
            }
        }
        else
        {
            var test = await _testService.GetTestAsync(model.EditQuestion.TestId);
            var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

            model.Test = test;
            model.Courses = courses;
            TempData["error"] = "Validation failed.";

            return View(model);
        }

        return RedirectToAction("Test", "Test", new { id = model.EditQuestion.TestId });
    }

    [HttpGet]
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> AddAnswer(Guid id, Guid questionId)
    {
        var test = await _testService.GetTestAsync(id);
        var question = await _testService.GetQuestionAsync(questionId);


        var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));

        var vm = new AddAnswerVM()
        {
            Test = test,
            Courses = courses,
            Queastion = question,
        };

        return this.View(vm);
    }

    [HttpPost]
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> AddAnswer(AddAnswerVM model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _testService.CreateAnswerAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), model.AddAnswer);
                TempData["success"] = "Answer added successfully.";
            }
            catch (Exception ex)
            {
                var question = await _testService.GetQuestionAsync(model.AddAnswer.QuestionId);
                var test = await _testService.GetTestAsync(model.AddAnswer.TestId);
                var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
                model.Courses = courses;
                model.Test = test;
                model.Queastion = question;

                _logger.LogError(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
                TempData["error"] = $"Error: {ex.Message}";
                return View(model);
            }
        }
        else
        {
            var question = await _testService.GetQuestionAsync(model.AddAnswer.QuestionId);
            var test = await _testService.GetTestAsync(model.AddAnswer.TestId);
            var courses = await _courseService.GetTeachersCoursesAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
            model.Courses = courses;
            model.Test = test;
            model.Queastion = question;

            TempData["error"] = "Validation failed.";
            return View(model);
        }

        return RedirectToAction("Test", "Test", new { id = model.AddAnswer.TestId });
    }

    [HttpGet]
    [Authorize(Roles = RoleConstants.AdminRole + "," + RoleConstants.TeacherRole)]
    public async Task<IActionResult> DeleteAnswer(Guid id, Guid testId)
    {
        try
        {
            await _testService.DeleteAnswerAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), id);
            TempData["success"] = "Answer deleted successfully.";
        }
        catch (Exception ex)
        {
            TempData["error"] = $"Error: {ex.Message}";
            _logger.LogError(ex.Message);
        }

        return RedirectToAction("Test", "Test", new { id = testId });
    }

    [Authorize]
    public async Task<IActionResult> StartTest(Guid testId, Guid courseId)
    {
        try
        {
            var res  = await _testService.StartTest(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), testId);
            ViewBag.TimeExpire = DateTime.UtcNow.AddMinutes(res.DurationMinutes);
            return View(res);
        }
        catch (Exception ex)
        {
            TempData["error"] = $"Error: {ex.Message}";
            _logger.LogError(ex.Message);
            return RedirectToAction("StudentCourse", "Course" , new {id = courseId});

        }
    }

    [Authorize]
    public async Task<IActionResult> QuestionTest(Guid testId, Guid questionId, Guid questionAnswerId, string? answerOne, string? answersMulti, string? answer, double? secondsRemains )
    {
        try
        {
            var res = await _testService.GetQuestionTest(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), testId, questionId, questionAnswerId, answerOne, answersMulti,answer);
            ViewBag.TimeExpire = DateTime.UtcNow.AddSeconds((double)secondsRemains);
            return View(res);
        }
        catch (Exception ex)
        {
            TempData["error"] = $"Error: {ex.Message}";
            _logger.LogError(ex.Message);
            return RedirectToAction("StudentCourses");

        }
    }

    [Authorize]
    public async Task<IActionResult> FinishTest(Guid testId, Guid questionAnswerId, string? answerOne, string? answersMulti, string? answer)
    {
        try
        {
            var res = await _testService.FinishTest(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), testId, questionAnswerId,answerOne, answersMulti,answer);
            return View(res);
        }
        catch (Exception ex)
        {
            TempData["error"] = $"Error: {ex.Message}";
            _logger.LogError(ex.Message);
            return RedirectToAction("StudentCourses");
        }
    }
    
    [Authorize]
    public async Task<IActionResult> TestResult(Guid testId)
    {
        try
        {
            var res = await _testService.GetTestResultAsync(Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value), testId);
            return View(res);
        }
        catch (Exception ex)
        {
            TempData["error"] = $"Error: {ex.Message}";
            _logger.LogError(ex.Message);
            return RedirectToAction("StudentCourses");
        }
    }

}
