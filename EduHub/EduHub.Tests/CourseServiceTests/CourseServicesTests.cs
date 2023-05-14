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

namespace EduHub.Tests.CourseServiceTests;

public class CourseServicesTests
{
    private CourseService _courseService;
    private IMapper _mapper;
    private Mock<IUnitOfWork> _unitOfWorkMock;

    [SetUp]
    public void Setup()
    {

        _unitOfWorkMock = new Mock<IUnitOfWork>();

       // _unitOfWorkMock.Setup(x => x.TeacherRequests).Returns(_repositoryTeachersRequestsMock.Object);

        var configurationBuider = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); });
        _mapper = configurationBuider.CreateMapper();

        //_courseService = new CourseService(
        //    _unitOfWorkMock.Object,
        //    _mapper,
        //    _testService);
    }
}
