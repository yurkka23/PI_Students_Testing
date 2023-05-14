using AutoMapper;
using EduHub.Application.DTOs.TestResult;
using EduHub.Application.DTOs.User;
using EduHub.Application.Interfaces;
using EduHub.Application.Models.Profile;
using EduHub.Domain.Entities;
using EduHub.Domain.Exceptions;
using EduHub.Persistence.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduHub.Application.Services;

public class UserService : IUserService
{
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;


    public UserService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper,
        IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _mapper = mapper;
        _fileService = fileService;
    }

    public async Task<UserDTO> GetByIdAsync(Guid id)
    {
        var user = await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.Id == id, null);
        if (user == null) throw new NotFoundException($"User with id - {id} ");
        
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO> GetByEmailAsync(string email)
    {
        var user = await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.Email == email);
        if (user == null) throw new NotFoundException($"User with email - {email} ");

        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO> GetUserProfileAsync(Guid userId)
    {
        var user = await _unitOfWork.Users.GetFirstOrDefaultAsync(x => x.Id == userId);
        if (user == null) throw new NotFoundException($"User with id - {userId} ");


        return _mapper.Map<UserDTO>(user);
    }

    public async Task<bool> EditUserProfileAsync(Guid userId, EditProfileModel model)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) throw new NotFoundException($"User profile with id - {userId} ");

        if (user.UserName != model.UserName.Trim())
        {
            var checkIfUsernameExists =await _userManager.FindByNameAsync(model.UserName);
            if (checkIfUsernameExists is not null)
            {
                throw new BadRequestException("Such username already exists");
            }
        }

        if (user.Email != model.Email.Trim())
        {
            var checkIfEmailExists =await _userManager.FindByEmailAsync(model.Email);
            if (checkIfEmailExists is not null)
            {
                throw new BadRequestException("Such email already exists");
            }
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
        return true;
    }

    public async Task ChangePhotoAsync(Guid userId, ChangePhotoModel model)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user.UserImgUrl is not null)
        {
            _fileService.DeleteFile(user.UserImgUrl);
        }

        if (model.NewProfileImage is not null)
        {
            user.UserImgUrl = await _fileService.SaveFile(model.NewProfileImage);
        }

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

    public async Task<bool> BecomeTeacher(Guid userId, TeacherRequestModel model)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        var request = new TeacherRequest
        {
            Id = Guid.NewGuid(),
            Text = model.Text,
            FullName = user.FirstName + ' ' + user.LastName,
            UserId = userId,
            ProofImage = model?.ProofImage == null ? "" : await _fileService.SaveFile(model.ProofImage)
        };

        await _unitOfWork.TeacherRequests.InsertAsync(request);
        return  await _unitOfWork.SaveAsync(userId);
    }

    public async Task<bool> DeleteAcountAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            throw new BadRequestException("Sucn user doesn't exists");
        }

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);

            throw new BadRequestException(string.Join(", ", errors));
        }

        return true;
    }
    public async Task<UserDTO> GetTeacherProfileAsync(Guid id)
    {
        var teacher = await _unitOfWork.Users.GetFirstOrDefaultAsync(
            x => x.Id == id, 
            x => x.Include(x=>x.Courses));

        if (teacher == null) throw new NotFoundException($"Teacher profile with id - {id} ");

        return _mapper.Map<UserDTO>(teacher);
    }

    public async Task<UserDTO> GetStudentProfileAsync(Guid id)
    {
        var student = await _unitOfWork.Users.GetFirstOrDefaultAsync(
            x => x.Id == id,
            x => x.Include(x => x.StudentsCourses).ThenInclude(x => x.Course));

        if (student == null) throw new NotFoundException($"Student profile with id - {id} ");

        var res = _mapper.Map<UserDTO>(student);

        var testResults = await _unitOfWork.TestResults.GetAsync(
            x => x.StudentId == id,
            null,
            x => x.Include(x => x.Test).ThenInclude(x => x.Questions)
            .Include(x => x.Test).ThenInclude(x => x.Teacher),0,int.MaxValue);

        res.TestResults = _mapper.Map<IEnumerable<TestResultDTO>>(testResults);

        return res;
    }

}