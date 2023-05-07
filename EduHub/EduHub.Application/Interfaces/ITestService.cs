
using EduHub.Application.DTOs.Question;
using EduHub.Application.DTOs.Test;
using EduHub.Application.Models.Answer;
using EduHub.Application.Models.Question;
using EduHub.Application.Models.Test;

namespace EduHub.Application.Interfaces;

public interface ITestService
{
    Task<Guid> CreateTestAsync(Guid teacherId, Guid courseId, AddTestModel model);
    Task DeleteTestAsync(Guid teacherId, Guid testId);
    Task<TestDTO> GetTestAsync(Guid testId);
    Task EditTestAsync(Guid teacherId, EditTestModel model);
    Task<Guid> CreateQuestionAsync(Guid teacherId, AddQuestionModel model);
    Task DeleteQuestionAsync(Guid teacherId, Guid questionId);
    Task<QuestionDTO> GetQuestionAsync(Guid questionId);
    Task EditQuestionAsync(Guid teacherId, EditQuestionModel model);
    Task<Guid> CreateAnswerAsync(Guid teacherId, AddAnswerModel model);
    Task DeleteAnswerAsync(Guid teacherId, Guid answerId);
    Task<PassingTestDTO> StartTest(Guid studentId, Guid testId);
    Task<PassingTestDTO> GetQuestionTest(Guid studentId, Guid testId,Guid questionId);

}
