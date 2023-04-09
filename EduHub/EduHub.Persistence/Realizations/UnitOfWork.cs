using EduHub.Domain.Entities;
using EduHub.Persistence.Abstractions;
using EduHub.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EduHub.Persistence.Realizations;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UnitOfWork> _logger;


    private BaseRepository<User> _users;
    private BaseRepository<Course> _courses;
    private BaseRepository<Test> _tests;
    private BaseRepository<Question> _questions;
    private BaseRepository<AnswerOption> _answerOptions;
    private BaseRepository<TestResult> _testResults;
    private BaseRepository<UserCourse> _userCourses;
    private BaseRepository<TeacherRequest> _teacherRequests;

    public UnitOfWork(ApplicationDbContext context, ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;
    }
    public IRepositoryAsync<User> Users
    {
        get
        {
            return _users ??= new BaseRepository<User>(_context);
        }
    }
    public IRepositoryAsync<Course> Courses
    {
        get
        {
            return _courses ??= new BaseRepository<Course>(_context);
        }
    }
    public IRepositoryAsync<Test> Tests
    {
        get
        {
            return _tests ??= new BaseRepository<Test>(_context);
        }
    }
    public IRepositoryAsync<Question> Questions
    {
        get
        {
            return _questions ??= new BaseRepository<Question>(_context);
        }
    }
    public IRepositoryAsync<AnswerOption> AnswerOptions
    {
        get
        {
            return _answerOptions ??= new BaseRepository<AnswerOption>(_context);
        }
    }
    public IRepositoryAsync<TestResult> TestResults
    {
        get
        {
            return _testResults ??= new BaseRepository<TestResult>(_context);
        }
    }
    public IRepositoryAsync<UserCourse> UserCourses
    {
        get
        {
            return _userCourses ??= new BaseRepository<UserCourse>(_context);
        }
    }
    public IRepositoryAsync<TeacherRequest> TeacherRequests
    {
        get
        {
            return _teacherRequests ??= new BaseRepository<TeacherRequest>(_context);
        }
    }

    public async Task<bool> SaveAsync(Guid userId)
    {
        var entities = _context.ChangeTracker.Entries();

        try
        {
            if (!entities.Any())
            {
                return Convert.ToBoolean(await _context.SaveChangesAsync());
            }

            foreach (var entity in entities)
            {
                if (entity.Entity is not BaseEntity baseEntity)
                {
                    continue;
                }

                if (entity.State == EntityState.Added)
                {
                    baseEntity.CreatedBy = userId;
                    baseEntity.CreatedAt = DateTime.UtcNow;
                }
                else if (entity.State == EntityState.Modified)
                {
                    baseEntity.UpdatedBy = userId;
                    baseEntity.UpdatedAt = DateTime.UtcNow;
                }
            }
            return Convert.ToBoolean(await _context.SaveChangesAsync());
        }
        catch (Exception ex)
        {

            _logger.LogError(ex.Message);
            _logger.LogError(ex.InnerException?.Message);

            return false;
        }
    }   
}
