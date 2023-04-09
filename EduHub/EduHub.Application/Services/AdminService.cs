using AutoMapper;
using EduHub.Application.DTOs.Admin;
using EduHub.Application.DTOs.User;
using EduHub.Application.Interfaces;
using EduHub.Domain.Constants;
using EduHub.Domain.Entities;
using EduHub.Domain.Exceptions;
using EduHub.Persistence.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EduHub.Application.Services;

public class AdminService : IAdminService
{
    private readonly IUnitOfWork _unitOfWork;
    private UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public AdminService(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper, IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _mapper = mapper;
        _fileService = fileService;
    }

    public async Task<IEnumerable<TeacherRequestDTO>> GetTeachersRequestsAsync()
    {
        var requests = await _unitOfWork.TeacherRequests.GetAsync();

        return _mapper.Map<IEnumerable<TeacherRequestDTO>>(requests);
    }
    public async Task AddTeacherAsync(Guid teacherId, Guid adminId)
    {
        var user = await _userManager.FindByIdAsync(teacherId.ToString());

        if (user is null) throw new BadRequestException("Such user doesn't exists");

        var result = await _userManager.RemoveFromRoleAsync(user, RoleConstants.StudentRole);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);

            throw new BadRequestException(string.Join(", ", errors));
        }

        var result1 = await _userManager.AddToRoleAsync(user, RoleConstants.TeacherRole);

        if (!result1.Succeeded)
        {
            var errors = result1.Errors.Select(e => e.Description);

            throw new BadRequestException(string.Join(", ", errors));
        }

        var deleteReuests = await _unitOfWork.TeacherRequests.GetAsync(t => t.UserId == teacherId);
        foreach(var userDelete in deleteReuests)
        {
            _unitOfWork.TeacherRequests.Delete(userDelete);
        }
        var res = _unitOfWork.SaveAsync(adminId);
    }
    public async Task RemoveFromTeacherAsync(Guid teacherId)
    {
        var user = await _userManager.FindByIdAsync(teacherId.ToString());

        if (user is null) throw new BadRequestException("Such user doesn't exists");

        var result = await _userManager.RemoveFromRoleAsync(user, RoleConstants.TeacherRole);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);

            throw new BadRequestException(string.Join(", ", errors));
        }

        var result1 = await _userManager.AddToRoleAsync(user, RoleConstants.StudentRole);

        if (!result1.Succeeded)
        {
            var errors = result1.Errors.Select(e => e.Description);

            throw new BadRequestException(string.Join(", ", errors));
        }
    }

    public async Task DenyTeacherAsync(Guid teacherId, Guid adminId)
    {
        var user = await _userManager.FindByIdAsync(teacherId.ToString());

        if (user is null) throw new BadRequestException("Such user doesn't exists");

        var deleteReuests = await _unitOfWork.TeacherRequests.GetAsync(t => t.UserId == teacherId);
        foreach (var userDelete in deleteReuests)
        {
            _unitOfWork.TeacherRequests.Delete(userDelete);
        }
        var res = _unitOfWork.SaveAsync(adminId);
    }

    public async Task<(IEnumerable<UserDTO> teachers, int count)> GetTeachersAsync(int pageNum, int _sizeLimit, string search)
    {
        if (search != null)
        {
            pageNum = 1;

            var teachersSearch = _mapper.Map<IEnumerable<UserDTO>>((await _userManager.GetUsersInRoleAsync(RoleConstants.TeacherRole))
                .Where(t => t.Email.Contains(search) || t.FirstName.Contains(search) || t.LastName.Contains(search) || t.UserName.Contains(search)));

            var countSearch = (await _userManager.GetUsersInRoleAsync(RoleConstants.TeacherRole))
                .Where(t => t.Email.Contains(search) || t.FirstName.Contains(search) || t.LastName.Contains(search) || t.UserName.Contains(search))
                .Count();

            return (teachersSearch, countSearch);
        }

        int count = (await _userManager.GetUsersInRoleAsync(RoleConstants.TeacherRole))
                .Count();

        var teachers = _mapper.Map<IEnumerable<UserDTO>>((await _userManager.GetUsersInRoleAsync(RoleConstants.TeacherRole))
           .Skip((pageNum - 1) * pageNum).Take(_sizeLimit));

        return (teachers, count);
    }
    public async Task<(IEnumerable<UserDTO> students, int count)> GetStudentsAsync(int pageNum, int _sizeLimit, string search)
    {
        if (search != null)
        {
            pageNum = 1;

            var studentsSearch = _mapper.Map<IEnumerable<UserDTO>>((await _userManager.GetUsersInRoleAsync(RoleConstants.StudentRole))
                .Where(t => t.Email.Contains(search) || t.FirstName.Contains(search) || t.LastName.Contains(search) || t.UserName.Contains(search)));
              
            var countSearch = (await _userManager.GetUsersInRoleAsync(RoleConstants.StudentRole))
                .Where(t => t.Email.Contains(search) || t.FirstName.Contains(search) || t.LastName.Contains(search) || t.UserName.Contains(search))
                .Count();

            return (studentsSearch, countSearch);
        }

        int count = (await _userManager.GetUsersInRoleAsync(RoleConstants.StudentRole))
                .Count();
  
        var students = _mapper.Map<IEnumerable<UserDTO>>((await _userManager.GetUsersInRoleAsync(RoleConstants.StudentRole))
            .Skip((pageNum - 1) * pageNum).Take(_sizeLimit));
        
        return (students, count);
    }

}
