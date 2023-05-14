using EduHub.Application.DTOs.User;
using EduHub.Application.Models.Profile;

namespace EduHub.Application.Interfaces;

public interface IUserService
{
    Task<UserDTO> GetByIdAsync(Guid id);
    Task<UserDTO> GetByEmailAsync(string email);
    Task<bool> EditUserProfileAsync(Guid userId, EditProfileModel model);
    Task<UserDTO> GetUserProfileAsync(Guid userId);
    Task ChangePhotoAsync(Guid userId, ChangePhotoModel model);
    Task ChangePasswordAsync(Guid userId, ChangePasswordModel model);
    Task<bool> BecomeTeacher(Guid userId, TeacherRequestModel model);
    Task<bool> DeleteAcountAsync(Guid userId);
    Task<UserDTO> GetTeacherProfileAsync(Guid id);
    Task<UserDTO> GetStudentProfileAsync(Guid id);
    
}