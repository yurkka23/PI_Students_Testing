//using AutoMapper;
//using EduHub.Application.Interfaces;
//using EduHub.Application.Mapper;
//using EduHub.Application.Services;
//using EduHub.Domain.Entities;
//using EduHub.Persistence.Abstractions;
//using EduHub.Tests.Common;
//using Microsoft.AspNetCore.Identity;
//using Moq;
//using NUnit.Framework;

//namespace EduHub.Tests.UserServiceTest;

//internal class GetUser
//{
//    private Mock<IUnitOfWork> _unitOfWorkMock;
//    private IMapper _mapper;
//    private Mock<UserManager<User>> _userManagerMock;
//    private UserService _userService;
//    private Mock<IRepositoryAsync<User>> _repositoryUserMock;
//    private Mock<IFileService> _fileServiceMock;

//    [SetUp]
//    public void Setup()
//    {
//        var userStoreMock = Mock.Of<IUserStore<User>>();
//        var configurationBuider = new MapperConfiguration(cfg =>
//        {
//            cfg.AddProfile(new AutoMapperProfile());
//        });
//        _fileServiceMock = new Mock<IFileService>();
//        _userManagerMock = MockUserManager.GetMock();
//        _repositoryUserMock = MockUserRepository.GetMock();
//        _unitOfWorkMock = new Mock<IUnitOfWork>();
//        _unitOfWorkMock.Setup(x => x.Users)
//            .Returns(_repositoryUserMock.Object);
//        _mapper = configurationBuider.CreateMapper();

//        _userService = new UserService(
//               _unitOfWorkMock.Object,
//               _userManagerMock.Object,
//               _mapper,
//               _fileServiceMock.Object
//               );
//    }
//    [TestCase]
//    public async Task GetTeacher_InvokeMethod_CheckIfRepoIsCalled()//cahnge
//    {
//        //Arrange
//        var userId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA155");

//        //Act

//        //Assert
//        //  Assert.NotNull(res);
//        //  Assert.AreEqual(res.UserName, "yurka23");
//    }

//    [TestCase]
//    public async Task GetTeacher_InvokeMethod_CheckIfRepoIsCalled1()//cahnge
//    {
//        //Arrange
//        var userId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA155");
//        //Act
//        var res = await _userService.GetByIdAsync(userId);

//        //Assert
//        Assert.NotNull(res);
//        Assert.AreEqual(res.UserName, "yurka23");
//    }
//}

