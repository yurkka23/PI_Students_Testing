using EduHub.Application.DTOs.User;
using EduHub.Application.Models.Profile;

namespace EduHub.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetByIdAsync(Guid id);
        Task<UserDTO> GetByEmailAsync(string email);
        Task EditUserProfileAsync(Guid userId, EditProfileModel model);
        Task<UserDTO> GetUserProfileAsync(Guid userId);
        Task ChangePhotoAsync(Guid userId, ChangePhotoModel model);
        Task ChangePasswordAsync(Guid userId, ChangePasswordModel model);
        Task BecomeTeacher(Guid userId, TeacherRequestModel model);
        Task DeleteAcountAsync(Guid userId);


        //Admin
        //Task CreateUserAsync(User user, string password);
        //Task UpdateUserAsync(User user);
        //Task DeleteUserAsync(User user);
    }
}