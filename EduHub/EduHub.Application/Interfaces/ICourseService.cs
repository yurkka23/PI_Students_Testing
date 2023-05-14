using EduHub.Application.DTOs.Course;
using EduHub.Application.Models.Course;

namespace EduHub.Application.Interfaces;

public interface ICourseService
{
    Task<Guid> CreateCourseAsync(Guid teacherId, CreateCourseModel model);
    Task EditCourseAsync(Guid teacherId, EditCourseModel model);
    Task DeleteCourseAsync(Guid teacherId, Guid courseId);
    Task<IEnumerable<CourseDTO>> GetTeachersCoursesAsync(Guid teacherId);
    Task<CourseDTO> GetCourseAsync(Guid id);
    Task<IEnumerable<CourseDTO>> GetCoursesAsync(string search);
    Task EnrollToCourseWithOutPassword(Guid studentId, Guid courseId);
    Task EnrollToCourseWithPassword(Guid studentId, Guid courseId, string password);
    Task<IEnumerable<CourseDTO>> GetStudentsCoursesAsync(Guid studentId);
    Task LeaveCourseAsync(Guid studentId, Guid courseId);


}