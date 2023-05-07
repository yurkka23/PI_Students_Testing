using AutoMapper;
using EduHub.Application.Mapper;
using EduHub.Application.Services;
using EduHub.Domain.Entities;
using EduHub.Persistence.Abstractions;
using EduHub.Tests.Common;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;

namespace EduHub.Tests.AdminServiceTest
{
    internal class GetTeachersRequests
    {
        private AdminService _adminService;
        private IMapper _mapper;
        private Mock<IRepositoryAsync<TeacherRequest>> _repositoryTeachersRequestsMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<UserManager<User>> _userManagerMock;

        [SetUp]
        public void Setup()
        {
            var userStoreMock = Mock.Of<IUserStore<User>>();
            var configurationBuider = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); });
            _userManagerMock =
                new Mock<UserManager<User>>(userStoreMock, null, null, null, null, null, null, null, null);

            _repositoryTeachersRequestsMock = MockTeacherRequestRepository.GetMock();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(x => x.TeacherRequests)
                .Returns(_repositoryTeachersRequestsMock.Object);
            _mapper = configurationBuider.CreateMapper();

            _adminService = new AdminService(
                _unitOfWorkMock.Object,
                _userManagerMock.Object,
                _mapper);
        }

        [TestCase]
        public async Task GetTeaches_Equal_Success()
        {
            //Arrange

            //Act
            var res = await _adminService.GetTeachersRequestsAsync();

            //Assert
            Assert.NotNull(res);
            Assert.AreEqual(res.Count(), 4);
        }

        [TestCase]
        public async Task GetTeaches_NotEqual_Success()
        {
            //Arrange

            //Act
            var res = await _adminService.GetTeachersRequestsAsync();

            //Assert
            Assert.NotNull(res);
            Assert.AreNotEqual(res.Count(), 3);
        }
    }
}