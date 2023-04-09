using EduHub.Application.Interfaces;
using EduHub.Persistence.Abstractions;
using Microsoft.AspNetCore.Identity;
using EduHub.Domain.Entities;
using EduHub.Application.DTOs.User;
using AutoMapper;
using EduHub.Application.Models.Profile;
using EduHub.Domain.Exceptions;

namespace EduHub.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;


    public UserService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper, IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _mapper = mapper;
        _fileService = fileService;
    }

    public async Task<UserDTO> GetByIdAsync(Guid id)
    {
        var user = await _unitOfWork.Users.GetFirstOrDefaultAsync(filter: x => x.Id == id);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO> GetByEmailAsync(string email)
    {
        var user = await _unitOfWork.Users.GetFirstOrDefaultAsync(filter: x => x.Email == email);

        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO> GetUserProfileAsync(Guid userId)
    {
        var user = await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.Id == userId);

        return _mapper.Map<UserDTO>(user);
    }
    public async Task EditUserProfileAsync(Guid userId, EditProfileModel model)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if(user.UserName != model.UserName.Trim())
        {
            var checkIfUsernameExists = _userManager.FindByNameAsync(model.UserName);
            if(checkIfUsernameExists is not null) throw new BadRequestException("Such username already exists");
        }

        if (user.Email != model.Email.Trim())
        {
            var checkIfEmailExists = _userManager.FindByEmailAsync(model.Email);
            if (checkIfEmailExists is not null) throw new BadRequestException("Such email already exists");
        }

        user.UserName = model.UserName;
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.AboutMe = model.AboutMe;
        user.Email = model.Email;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);

            throw new BadRequestException(string.Join(", ", errors));
        }
    }

    public async Task ChangePhotoAsync(Guid userId, ChangePhotoModel model)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user.UserImgUrl is not null) _fileService.DeleteFile(user.UserImgUrl);

        if (model.NewProfileImage is not null) user.UserImgUrl = await _fileService.SaveFile(model.NewProfileImage);

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);

            throw new BadRequestException(string.Join(", ", errors));
        }
    }

    public async Task ChangePasswordAsync(Guid userId, ChangePasswordModel model)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        var resetPassResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
        if (!resetPassResult.Succeeded)
        {
            var errors = resetPassResult.Errors.Select(e => e.Description);

            throw new BadRequestException(string.Join(", ", errors));
        }
    }

    public async Task BecomeTeacher(Guid userId, TeacherRequestModel model)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        var request = new TeacherRequest()
        {
            Id = Guid.NewGuid(),
            Text = model.Text,
            FullName = user.FirstName + ' ' + user.LastName,
            UserId = userId,
            ProofImage = await _fileService.SaveFile(model.ProofImage)
        };

        await _unitOfWork.TeacherRequests.InsertAsync(request);
        await _unitOfWork.SaveAsync(userId);
    }

    public async Task DeleteAcountAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null) throw new BadRequestException("Sucn user doesn't exists");
        
        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);

            throw new BadRequestException(string.Join(", ", errors));
        }
    }
}
