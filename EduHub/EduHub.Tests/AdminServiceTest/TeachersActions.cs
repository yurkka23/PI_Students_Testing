using AutoMapper;
using EduHub.Application.Mapper;
using EduHub.Application.Services;
using EduHub.Domain.Entities;
using EduHub.Domain.Exceptions;
using EduHub.Persistence.Abstractions;
using EduHub.Tests.Common;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;

namespace EduHub.Tests.AdminServiceTest
{
    internal class TeachersActions
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
            //_userManagerMock = new Mock<UserManager<User>>(userStoreMock, null, null, null, null, null, null, null, null);

            _userManagerMock =
                MockUserManager
                    .GetMock(); // new Mock<UserManager<User>>(userStoreMock, null, null, null, null, null, null, null, null);
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
        public void AddTeacher_ThrowException()
        {
            //Arrange
            var adminId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA156");
            //Act

            //Assert
            Assert.ThrowsAsync<BadRequestException>(
                async () => await _adminService.AddTeacherAsync(Guid.Empty, adminId));
        }

        [TestCase]
        public void RemoveFromTeacher_ThrowException()
        {
            //Arrange
            var teacherId = Guid.Empty;
            //Act

            //Assert
            Assert.ThrowsAsync<BadRequestException>(async () => await _adminService.RemoveFromTeacherAsync(teacherId));
        }

        [TestCase]
        public void DenyTeacher_ThrowException()
        {
            //Arrange
            var teacherId = Guid.Empty;
            var adminId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA156");

            //Act

            //Assert
            Assert.ThrowsAsync<BadRequestException>(
                async () => await _adminService.DenyTeacherAsync(teacherId, adminId));
        }
    }
}