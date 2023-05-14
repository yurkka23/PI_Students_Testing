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
using EduHub.Application.Interfaces;
using EduHub.Application.Models.Profile;

namespace EduHub.Tests.UserServiceTests;

public class UserServicesTests
{
    private UserService _userService;
    private FileService _fileService;
    private IMapper _mapper;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<UserManager<User>> _userManagerMock;
    private Mock<IRepositoryAsync<User>> _repositoryUserMock;
    private Mock<IRepositoryAsync<TestResult>> _repositoryTestResultMock;
    private Mock<IRepositoryAsync<TeacherRequest>> _repositoryTeachersRequestsMock;


    [SetUp]
    public void Setup()
    {
        var configurationBuider = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile());});
        _mapper = configurationBuider.CreateMapper();

        _userManagerMock = MockUserManager.GetMock();

        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _fileService = new FileService();

        _repositoryUserMock = MockUserRepository.GetMock();
        _repositoryTestResultMock = MockTestResultsRepository.GetMock();
        _repositoryTeachersRequestsMock = MockTeacherRequestRepository.GetMock();

        _unitOfWorkMock.Setup(x => x.Users).Returns(_repositoryUserMock.Object);
        _unitOfWorkMock.Setup(x => x.TestResults).Returns(_repositoryTestResultMock.Object);
        _unitOfWorkMock.Setup(x => x.TeacherRequests).Returns(_repositoryTeachersRequestsMock.Object);

        _userService = new UserService(
            _unitOfWorkMock.Object,
            _userManagerMock.Object,
            _mapper,
            _fileService);
    }

    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA155")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA158")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA159")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA160")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA156")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA162")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA157")]

    public async Task GetByIdAsync_WhenUserIdExists_ShouldReturnCorrectEqual(string id)
    {
        //Arrange

        //Act
        var res = await _userService.GetByIdAsync(Guid.Parse(id));

        //Assert
        Assert.NotNull(res);
        Assert.AreEqual(res.Id, Guid.Parse(id));
    }
    [TestCase]
    public void GetByIdAsync_WhenUserIdGuidEmpty_ThrowException()
    {
        //Arrange
        var id = Guid.Empty;

        //Act

        //Assert
        Assert.ThrowsAsync<NotFoundException>(
          async () => await _userService.GetByIdAsync(id));
    }
    [TestCase]
    public void GetByIdAsync_WhenUserIdNotExists_ThrowException()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act

        //Assert
        Assert.ThrowsAsync<NotFoundException>(
          async () => await _userService.GetByIdAsync(id));
    }
    
    [TestCase("urakarabin12@gmail.com")]
    [TestCase("igor56@gmail.com")]
    [TestCase("vika@gmail.com")]
    [TestCase("vika900@gmail.com")]
    [TestCase("vika223@gmail.com")]
    [TestCase("igor@gmail.com")]

    public async Task GetByEmailAsync_WhenUserWithEmailExists_ShouldReturnCorrectEqual(string email)
    {
        //Arrange

        //Act
        var res = await _userService.GetByEmailAsync(email);

        //Assert
        Assert.NotNull(res);
        Assert.AreEqual(res.Email, email);
    }
  
    [TestCase("nastia@gmail.com")]
    [TestCase("misha@gmail.com")]
    [TestCase("yura@gmail.com")]
    public void GetByEmailAsync_WhenUserEmailNotExists_ThrowException(string email)
    {
        //Arrange

        //Act

        //Assert
        Assert.ThrowsAsync<NotFoundException>(
          async () => await _userService.GetByEmailAsync(email));
    }

    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA155")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA158")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA159")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA160")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA156")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA162")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA157")]

    public async Task GetUserProfileAsync_WhenUserIdExists_ShouldReturnCorrectEqual(string id)
    {
        //Arrange

        //Act
        var res = await _userService.GetUserProfileAsync(Guid.Parse(id));

        //Assert
        Assert.NotNull(res);
        Assert.AreEqual(res.Id, Guid.Parse(id));
    }
    
    [TestCase]
    public void GetUserProfileAsync_WhenUserIdGuidEmpty_ThrowException()
    {
        //Arrange
        var id = Guid.Empty;

        //Act

        //Assert
        Assert.ThrowsAsync<NotFoundException>(
          async () => await _userService.GetUserProfileAsync(id));
    }
    [TestCase]
    public void GetUserProfileAsync_WhenUserIdNotExists_ThrowException()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act

        //Assert
        Assert.ThrowsAsync<NotFoundException>(
          async () => await _userService.GetUserProfileAsync(id));
    }

    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA155")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA158")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA159")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA160")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA156")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA162")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA157")]

    public async Task GetTeacherProfileAsync_WhenUserIdExists_ShouldReturnCorrectEqual(string id)
    {
        //Arrange

        //Act
        var res = await _userService.GetTeacherProfileAsync(Guid.Parse(id));

        //Assert
        Assert.NotNull(res);
        Assert.AreEqual(res.Id, Guid.Parse(id));
    }
    [TestCase]
    public void GetTeacherProfileAsync_WhenUserIdGuidEmpty_ThrowException()
    {
        //Arrange
        var id = Guid.Empty;

        //Act

        //Assert
        Assert.ThrowsAsync<NotFoundException>(
          async () => await _userService.GetTeacherProfileAsync(id));
    }
    [TestCase]
    public void GetTeacherProfileAsync_WhenUserIdNotExists_ThrowException()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act

        //Assert
        Assert.ThrowsAsync<NotFoundException>(
          async () => await _userService.GetTeacherProfileAsync(id));
    }

    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA155")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA158")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA159")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA160")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA156")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA162")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA157")]

    public async Task GetStudentProfileAsync_WhenUserIdExists_ShouldReturnCorrectEqual(string id)
    {
        //Arrange

        //Act
        var res = await _userService.GetStudentProfileAsync(Guid.Parse(id));

        //Assert
        Assert.NotNull(res);
        Assert.AreEqual(res.Id, Guid.Parse(id));
    }
    [TestCase]
    public void GetStudentProfileAsync_WhenUserIdGuidEmpty_ThrowException()
    {
        //Arrange
        var id = Guid.Empty;

        //Act

        //Assert
        Assert.ThrowsAsync<NotFoundException>(
          async () => await _userService.GetStudentProfileAsync(id));
    }
    [TestCase]
    public void GetStudentProfileAsync_WhenUserIdNotExists_ThrowException()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act

        //Assert
        Assert.ThrowsAsync<NotFoundException>(
          async () => await _userService.GetStudentProfileAsync(id));
    }
   
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA155")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA158")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA159")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA160")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA156")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA162")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA157")]
    public async Task DeleteAcountAsync_WhenUserExists_ShouldReturnCorrectEqual(string id)
    {
        //Arrange

        //Act
        var res = await _userService.DeleteAcountAsync(Guid.Parse(id));

        //Assert
        Assert.NotNull(res);
        Assert.IsTrue(res);
    }
    [TestCase]
    public void DeleteAcountAsync_WhenUserIdGuidEmpty_ThrowException()
    {
        //Arrange
        var id = Guid.Empty;

        //Act

        //Assert
        Assert.ThrowsAsync<BadRequestException>(
          async () => await _userService.DeleteAcountAsync(id));
    }
    [TestCase]
    public void DeleteAcountAsync_WhenUserIdNotExists_ThrowException()
    {
        //Arrange
        var id = Guid.NewGuid();

        //Act

        //Assert
        Assert.ThrowsAsync<BadRequestException>(
          async () => await _userService.DeleteAcountAsync(id));
    }

    //[TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA155")]
    //public async Task BecomeTeacher_Wh(string id)
    //{
    //    //Arrange
    //    var model = new TeacherRequestModel
    //    {
    //        Text = "I want be teacher",
    //       // ProofImage = 
    //    };

    //    //Act
    //    var res = await _userService.BecomeTeacher(Guid.Parse(id), model);

    //    //Assert
    //    Assert.NotNull(res);
    //    Assert.IsTrue(res);

    //}

    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA155")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA158")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA159")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA160")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA156")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA162")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA157")]
    public async Task EditUserProfileAsync_WhenUserExists_ShouldReturnCorrectEqual(string id)
    {
        //Arrange
        var model = new EditProfileModel
        {
           UserName = "new username 123",
           FirstName = "firstname",
           LastName = "lastname",
           AboutMe = "Edit profile",
           Email = "newemail123@gmail.com"
        };

        //Act
        var res = await _userService.EditUserProfileAsync(Guid.Parse(id), model);

        //Assert
        Assert.NotNull(res);
        Assert.IsTrue(res);
    }

    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA155")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA158")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA159")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA156")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA162")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA157")]
    public void EditUserProfileAsync_WhenUserNameExists_ThrowException(string id)
    {
        //Arrange
        var model = new EditProfileModel
        {
            UserName = "igor78",
            FirstName = "firstname",
            LastName = "lastname",
            AboutMe = "Edit profile",
            Email = "newemail123@gmail.com"
        };

        //Act
        //Assert
        Assert.ThrowsAsync<BadRequestException>(
         async () => await _userService.EditUserProfileAsync(Guid.Parse(id), model));

    }
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA158")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA159")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA160")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA156")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA162")]
    [TestCase("9D4C5258-DD24-4FF7-87A0-463FB3BAA157")]
    public void EditUserProfileAsync_WhenEmailExists_ThrowException(string id)
    {
        //Arrange
        var model = new EditProfileModel
        {
            UserName = "new username 234",
            FirstName = "firstname",
            LastName = "lastname",
            AboutMe = "Edit profile",
            Email = "urakarabin12@gmail.com"
        };

        //Act
        //Assert
        Assert.ThrowsAsync<BadRequestException>(
         async () => await _userService.EditUserProfileAsync(Guid.Parse(id), model));

    }
    [TestCase]
    public void EditUserProfileAsync_WhenUserIdGuidEmpty_ThrowException()
    {
        //Arrange
        var id = Guid.Empty;
        var model = new EditProfileModel
        {
            UserName = "new username 123",
            FirstName = "firstname",
            LastName = "lastname",
            AboutMe = "Edit profile",
            Email = "newemail123@gmail.com"
        };

        //Act

        //Assert
        Assert.ThrowsAsync<NotFoundException>(
          async () => await _userService.EditUserProfileAsync(id, model));
    }
    [TestCase]
    public void EditUserProfileAsync_WhenUserIdNotExists_ThrowException()
    {
        //Arrange
        var id = Guid.NewGuid();
        var model = new EditProfileModel
        {
            UserName = "new username 123",
            FirstName = "firstname",
            LastName = "lastname",
            AboutMe = "Edit profile",
            Email = "newemail123@gmail.com"
        };

        //Act

        //Assert
        Assert.ThrowsAsync<NotFoundException>(
          async () => await _userService.EditUserProfileAsync(id, model));
    }
}
