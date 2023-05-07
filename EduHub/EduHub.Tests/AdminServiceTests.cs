//using AutoMapper;
//using EduHub.Application.Mapper;
//using EduHub.Application.Services;
//using EduHub.Domain.Constants;
//using EduHub.Domain.Entities;
//using EduHub.Persistence.Abstractions;
//using EduHub.Persistence.Realizations;
//using Microsoft.AspNetCore.Identity;
//using Moq;
//using NUnit.Framework;

//namespace EduHub.Tests;


//[TestFixture]
//public class AdminServiceTests
//{

//   // private readonly IUnitOfWork _unitOfWork;
//   // private UserManager<User> _userManager;
//   /// private readonly IMapper _mapper;

//    private Mock<IUnitOfWork> _unitOfWorkMock;

//    private IMapper _mapper;
//    private Mock<UserManager<User>> _userManagerMock;
//    private AdminService _adminService;


//    [SetUp]
//    public void Setup()
//    {
//        var UserStoreMock = Mock.Of<IUserStore<User>>();
//        var userMgr = new Mock<UserManager<User>>(UserStoreMock, null, null, null, null, null, null, null, null);
//        //var repositoryTeacherReuestMock =new Mock<IRepositoryAsync<TeacherRequest>>(); 
//        var repositoryTeacherReuestMock = MockTeacherRequestRepository.GetMock();
//        var user = new User() { Id = Guid.Parse("D1800BD2-5C37-408F-A3CB-6AFB635B4717"), UserName = "f00", Email = "f00@example.com" };
//        var tcs = new TaskCompletionSource<User>();
//        tcs.SetResult(user);

//        //var teacherRequest = new TeacherRequest
//        //{
//        //    Id = Guid.Parse("D1800BD2-5C37-408F-A3CB-6AFB635B4717"),
//        //    Text = "my test"
//        //};
//        //var teacherRequest1 = new TeacherRequest
//        //{
//        //    Id = Guid.Parse("D1800BD2-5C37-408F-A3CB-6AFB635B4719"),
//        //    Text = "my test1"
//        //};
//        //var list =new List<TeacherRequest>();
//        //list.Add(teacherRequest);
//        //list.Add(teacherRequest1);

//        //var baserepo = BaseRepository<TeacherRequest>();
//       //var tcsreq = new TaskCompletionSource<IEnumerable<TeacherRequest>>();

//       // tcsreq.SetResult(list);

//        //userMgr.Setup(x => x.FindByIdAsync("f00")).Returns(tcs.Task);
//        //

//        _unitOfWorkMock = new Mock<IUnitOfWork>();
//        _unitOfWorkMock.Setup(x => x.TeacherRequests)
//            .Returns(repositoryTeacherReuestMock.Object);

//       // repositoryTeacherReuestMock.Setup(x => x.GetAsync(null, null, null, 0, 10)).Returns(tcsreq.Task);


//        var configurationBuider = new MapperConfiguration(cfg =>
//        {
//            cfg.AddProfile(new AutoMapperProfile());
//        });
//        _mapper = configurationBuider.CreateMapper();
//        //var store = new Mock<User>();

//        //var mgr = new UserManager<User>(store, null, null, null, null, null, null, null, null);

//        //_userManagerMock = new Mock<UserManager<User>>();

//        userMgr.Setup(userManager =>  userManager.FindByIdAsync("D1800BD2-5C37-408F-A3CB-6AFB635B4717"))
//               .Returns(tcs.Task);

//        userMgr.Setup(userManager => userManager.IsInRoleAsync(It.IsAny<User>(), RoleConstants.TeacherRole))
//            .ReturnsAsync(true);

//        //userMgr.Setup(userManager => userManager.GetUsersInRoleAsync(RoleConstants.TeacherRole).
//        //   .ReturnsAsync(true);

//        //int count = (await _userManager.GetUsersInRoleAsync(RoleConstants.TeacherRole))
//        //       .Count();

//        _adminService = new AdminService(
//               _unitOfWorkMock.Object,
//               userMgr.Object,
//               _mapper);

//    }

//    [TestCase]
//    public async Task GetTeacher_InvokeMethod_CheckIfRepoIsCalled()//cahnge
//    {
//        //var res = await _adminService.GetTeachersAsync(1, 5, null);
//        var res = await _adminService.GetTeachersRequestsAsync();
//        Assert.NotNull(res);
//        //_studyRoomBookingRepoMock.Verify(x => x.GetAll(null), Times.Once);
//    }
//}

//internal class MockTeacherRequestRepository
//{

//    public static Mock<IRepositoryAsync<TeacherRequest>> GetMock()
//    {
//        var mock = new Mock<IRepositoryAsync<TeacherRequest>>();
//        var requests = new List<TeacherRequest>()
//        {
//            new TeacherRequest()
//            {
//                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
//                Text = "text",
//                FullName = "teacher",

//            },
//            new TeacherRequest()
//            {
//                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728951e"),
//                Text = "text1",
//                FullName = "teacher1",

//            }
//        };
//        var tcsreq = new TaskCompletionSource<IEnumerable<TeacherRequest>>();

//        tcsreq.SetResult(requests);

//        mock.Setup(m => m.GetAsync(null, null, null, 0, 10)).Returns(() => tcsreq.Task);

//        // Set up


//        //userMgr.Setup(x => x.FindByIdAsync("f00")).Returns(tcs.Task);

//        return mock;
//    }
//}

