using EduHub.Domain.Entities;

namespace EduHub.Persistence.Abstractions
{
    public interface IUnitOfWork
    {
        IRepositoryAsync<User> Users { get; }
        IRepositoryAsync<Course> Courses { get; }
        IRepositoryAsync<Test> Tests { get; }
        IRepositoryAsync<Question> Questions { get; }
        IRepositoryAsync<AnswerOption> AnswerOptions { get; }
        IRepositoryAsync<TestResult> TestResults { get; }
        IRepositoryAsync<UserCourse> UserCourses { get; }
        IRepositoryAsync<TeacherRequest> TeacherRequests { get; }
        IRepositoryAsync<PassingTest> PassingTests { get; }
        IRepositoryAsync<QuestionAnswer> QuestionAnswers { get; }
        Task<bool> SaveAsync(Guid userId);
    }
}