using AutoMapper;
using EduHub.Application.Mapper;
using EduHub.Application.Services;
using EduHub.Domain.Entities;
using EduHub.Persistence.Abstractions;
using EduHub.Tests.Common;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using EduHub.Domain.Exceptions;

namespace EduHub.Tests.AdminServiceTests;

public class AdminServicesTests
{
    private AdminService _adminService;
    private IMapper _mapper;
    private Mock<IRepositoryAsync<TeacherRequest>> _repositoryTeachersRequestsMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<UserManager<User>> _userManagerMock;

    [SetUp]
    public void Setup()
    {
        var configurationBuider = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile());});
        _mapper = configurationBuider.CreateMapper();

        _userManagerMock = MockUserManager.GetMock(); 

        _repositoryTeachersRequestsMock = MockTeacherRequestRepository.GetMock();

        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _unitOfWorkMock.Setup(x => x.TeacherRequests).Returns(_repositoryTeachersRequestsMock.Object);

        _adminService = new AdminService(
            _unitOfWorkMock.Object,
            _userManagerMock.Object,
            _mapper);
    }
   
    [TestCase]
    public async Task GetTeachesRequestsAsync_WhenTeachersAre4_ShouldReturnCorrectEqual()
    {
        //Arrange

        //Act
        var res = await _adminService.GetTeachersRequestsAsync();

        //Assert
        Assert.NotNull(res);
        Assert.AreEqual(res.Count(), 4);
    }

    [TestCase]
    public async Task GetTeachesRequestsAsync_WhenTeachersAre4_ShouldReturnCorrectNotEqual()
    {
        //Arrange

        //Act
        var res = await _adminService.GetTeachersRequestsAsync();

        //Assert
        Assert.NotNull(res);
        Assert.AreNotEqual(res.Count(), 3);
    }


    [TestCase]
    public void AddTeacherAsync_WhenIdNotExists_ThrowException()
    {
        //Arrange
        var adminId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA156");
        //Act

        //Assert
        Assert.ThrowsAsync<BadRequestException>(
            async () => await _adminService.AddTeacherAsync(Guid.Empty, adminId));
    }

    [TestCase]
    public void RemoveFromTeacherAsync_WhenIdEmpty_ThrowException()
    {
        //Arrange
        var teacherId = Guid.Empty;
        //Act

        //Assert
        Assert.ThrowsAsync<BadRequestException>(async () => await _adminService.RemoveFromTeacherAsync(teacherId));
    }

    [TestCase]
    public void DenyTeacherAsync_WhenTeacherIdEmpty_ThrowException()
    {
        //Arrange
        var teacherId = Guid.Empty;
        var adminId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA156");

        //Act

        //Assert
        Assert.ThrowsAsync<BadRequestException>(
            async () => await _adminService.DenyTeacherAsync(teacherId, adminId));
    }

    [TestCase]
    public async Task GetStudentsAsync_WhenIsSearchTerm_ShouldReturnCorrectTotal()
    {
        //Arrange
        var pageNum = 1;
        var sizeLimit = 10;
        var searchTerm = "ig";

        //Act
        var res = await _adminService.GetStudentsAsync(pageNum, sizeLimit,searchTerm);

        //Assert
        Assert.NotNull(res);
        Assert.AreEqual(res.students.Count(), 3);

    }

    [TestCase(1,2)]
    [TestCase(2, 2)]
    [TestCase(1, 4)]
    public async Task GetStudentsAsync_WhenSearchTermIsNull_ShouldReturnCorrectTotal(int pageNum,int sizeLimit)
    {
        //Arrange

        //Act
        var res = await _adminService.GetStudentsAsync(pageNum, sizeLimit, null);

        //Assert
        Assert.NotNull(res);
        Assert.AreEqual(res.students.Count(), sizeLimit);
    }

    [TestCase]
    public async Task GetTeachersAsync_WhenIsSearchTerm_ShouldReturnCorrectTotal()
    {
        //Arrange
        var pageNum = 1;
        var sizeLimit = 10;
        var searchTerm = "vi";

        //Act
        var res = await _adminService.GetTeachersAsync(pageNum, sizeLimit, searchTerm);

        //Assert
        Assert.NotNull(res);
        Assert.AreEqual(res.teachers.Count(), 3);

    }

    [TestCase(1, 2)]
    [TestCase(2, 1)]
    [TestCase(1, 3)]
    public async Task GetTeachersAsync_WhenSearchTermIsNull_ShouldReturnCorrectTotal(int pageNum, int sizeLimit)
    {
        //Arrange

        //Act
        var res = await _adminService.GetTeachersAsync(pageNum, sizeLimit, null);

        //Assert
        Assert.NotNull(res);
        Assert.AreEqual(res.teachers.Count(), sizeLimit);
    }
}

