using AutoMapper;
using EduHub.Application.DTOs.Course;
using EduHub.Application.Interfaces;
using EduHub.Application.Models.Course;
using EduHub.Domain.Entities;
using EduHub.Domain.Exceptions;
using EduHub.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EduHub.Application.Services;

public class CourseService : ICourseService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITestService _testService;

    public CourseService(IUnitOfWork unitOfWork, IMapper mapper, ITestService testService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _testService = testService; 
    }

    public async Task<Guid> CreateCourseAsync(Guid teacherId, CreateCourseModel model)
    {
        var course = new Course
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Description = model.Description,
            Password = model.Password,
            TeacherId = teacherId
        };

        await _unitOfWork.Courses.InsertAsync(course);

        await _unitOfWork.SaveAsync(teacherId);

        return course.Id;
    }

    public async Task EditCourseAsync(Guid teacherId, EditCourseModel model)
    {
        var course = await _unitOfWork.Courses.GetFirstOrDefaultAsync(x => x.Id == model.Id);
        if (course == null)
        {
            throw new NotFoundException($"Course with id - {model.Id} ");
        }

        course.Name = model.Name;
        course.Description = model.Description;
        course.Password = model.Password;

        _unitOfWork.Courses.Update(course);

        await _unitOfWork.SaveAsync(teacherId);
    }

    public async Task DeleteCourseAsync(Guid teacherId, Guid courseId)
    {
        var course = await _unitOfWork.Courses.GetFirstOrDefaultAsync(x => x.Id == courseId);
        if (course == null)
        {
            throw new NotFoundException($"Course with id - {courseId} ");
        }
        var tests = await _unitOfWork.Tests.GetAsync(x => x.CourseId == courseId);
        foreach (var test in tests)
        {
            await _testService.DeleteTestAsync(teacherId, test.Id);
        }
        _unitOfWork.Courses.Delete(course);

        await _unitOfWork.SaveAsync(teacherId);
    }

    public async Task<IEnumerable<CourseDTO>> GetTeachersCoursesAsync(Guid teacherId)
    {
        var courses = await _unitOfWork.Courses.GetAsync(
            x => x.TeacherId == teacherId,
            x => x.OrderBy(x => x.Name),
            x => x.Include(x => x.Teacher));

        return _mapper.Map<IEnumerable<CourseDTO>>(courses);
    }

    public async Task<CourseDTO> GetCourseAsync(Guid id)
    {
        var course = await _unitOfWork.Courses.GetFirstOrDefaultAsync(
            x => x.Id == id,
            x => x.Include(x => x.Teacher).Include(x => x.Tests).ThenInclude(x => x.Questions));

        return _mapper.Map<CourseDTO>(course);
    }
    public async Task<IEnumerable<CourseDTO>> GetCoursesAsync(string search)
    {
        var courses = await _unitOfWork.Courses.GetAsync(x => x.Name.Contains(search ?? ""),
            null,
             x => x.Include(x => x.Teacher).Include(x => x.Tests));

        return _mapper.Map<IEnumerable<CourseDTO>>(courses);
    }
    public async Task EnrollToCourseWithOutPassword(Guid studentId, Guid courseId)
    {
        var course = await _unitOfWork.Courses.GetFirstOrDefaultAsync(x => x.Id == courseId);
        if (course is null) throw new NotFoundException($"Course with Id - {courseId} ");

        var courseStudent = new UserCourse
        {
            CourseId = courseId,
            StudentId = studentId,
            AddedToCourse = DateTimeOffset.Now
        };

        await _unitOfWork.UserCourses.InsertAsync(courseStudent);
        var res = await _unitOfWork.SaveAsync(studentId);
        if (!res) throw new Exception($"You already have this course!");

    }
    public async Task EnrollToCourseWithPassword(Guid studentId, Guid courseId, string password)
    {
        var course = await _unitOfWork.Courses.GetFirstOrDefaultAsync(x => x.Id == courseId);
        if (course is null) throw new NotFoundException($"Course with Id - {courseId} ");
        if (course.Password != password) throw new BadRequestException($"Password wrong");

        var courseStudent = new UserCourse
        {
            CourseId = courseId,
            StudentId = studentId,
            AddedToCourse = DateTimeOffset.Now
        };

        await _unitOfWork.UserCourses.InsertAsync(courseStudent);
        var res = await _unitOfWork.SaveAsync(studentId);
        if(!res) throw new Exception($"You already have this course!");
    }
    public async Task<IEnumerable<CourseDTO>> GetStudentsCoursesAsync(Guid studentId)
    {
        var coursesIds = (await _unitOfWork.UserCourses.GetAsync(x => x.StudentId == studentId)).Select(x=> x.CourseId);

        var courses = await _unitOfWork.Courses.GetAsync(
          x => coursesIds.Contains(x.Id),
          x => x.OrderBy(x => x.Name),
          x => x.Include(x => x.Teacher));

        return _mapper.Map<IEnumerable<CourseDTO>>(courses);
    }

    public async Task LeaveCourseAsync(Guid studentId, Guid courseId)
    {
        var course = await _unitOfWork.UserCourses.GetFirstOrDefaultAsync(x => x.CourseId == courseId && x.StudentId == studentId);
        if (course == null)
        {
            throw new NotFoundException($"Course with id - {courseId} ");
        }
        
        _unitOfWork.UserCourses.Delete(course);

        await _unitOfWork.SaveAsync(studentId);
    }


}