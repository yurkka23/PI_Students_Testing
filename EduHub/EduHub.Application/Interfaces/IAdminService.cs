using EduHub.Application.DTOs.Admin;
using EduHub.Application.DTOs.User;

namespace EduHub.Application.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<TeacherRequestDTO>> GetTeachersRequestsAsync();
        Task AddTeacherAsync(Guid teacherId, Guid adminId);
        Task RemoveFromTeacherAsync(Guid teacherId);
        Task DenyTeacherAsync(Guid teacherId, Guid adminId);
        Task<(IEnumerable<UserDTO> teachers, int count)> GetTeachersAsync(int pageNum, int _sizeLimit, string search);
        Task<(IEnumerable<UserDTO> students, int count)> GetStudentsAsync(int pageNum, int _sizeLimit, string search);
    }
}