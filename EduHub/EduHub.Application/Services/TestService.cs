﻿using AutoMapper;
using EduHub.Application.DTOs.Question;
using EduHub.Application.DTOs.Test;
using EduHub.Application.Interfaces;
using EduHub.Application.Models.Answer;
using EduHub.Application.Models.Question;
using EduHub.Application.Models.Test;
using EduHub.Domain.Entities;
using EduHub.Domain.Exceptions;
using EduHub.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace EduHub.Application.Services;

public class TestService: ITestService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TestService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> CreateTestAsync(Guid teacherId, Guid courseId, AddTestModel model)
    {
        var test = new Test
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Description = model.Description,
            StartTime = model.StartTime,
            EndTime = model.EndTime,
            DurationMinutes = model.DurationMinutes,
            TeacherId = teacherId,
            CourseId = courseId
        };

        await _unitOfWork.Tests.InsertAsync(test);
        var res = await _unitOfWork.SaveAsync(teacherId);

        return test.Id;
    }
    public async Task DeleteTestAsync(Guid teacherId, Guid testId)
    {
        var test = await _unitOfWork.Tests.GetFirstOrDefaultAsync(x => x.Id == testId);
        if (test == null)
        {
            throw new NotFoundException($"Test with id - {testId} ");
        }
        var questions = await _unitOfWork.Questions.GetAsync(x => x.TestId == test.Id);

        foreach(var question in questions)
        {
            await this.DeleteQuestionAsync(teacherId, question.Id);
        }
        _unitOfWork.Tests.Delete(test);

        await _unitOfWork.SaveAsync(teacherId);
    }

    public async Task<TestDTO> GetTestAsync(Guid id)
    {
        var test = await _unitOfWork.Tests.GetFirstOrDefaultAsync(
            x => x.Id == id,
            x => x.Include(x => x.Teacher).Include(x => x.Questions).ThenInclude(x => x.Answers));

        return _mapper.Map<TestDTO>(test);
    }

    public async Task EditTestAsync(Guid teacherId, EditTestModel model)
    {
        var test = await _unitOfWork.Tests.GetFirstOrDefaultAsync(x => x.Id == model.Id);
        if (test == null)
        {
            throw new NotFoundException($"Test with id - {model.Id} ");
        }

        test.Name = model.Name;
        test.Description = model.Description;
        test.StartTime = model.StartTime;
        test.EndTime = model.EndTime;
        test.DurationMinutes = model.DurationMinutes;

        _unitOfWork.Tests.Update(test);

        await _unitOfWork.SaveAsync(teacherId);
    }

    public async Task<Guid> CreateQuestionAsync(Guid teacherId, AddQuestionModel model)
    {
        var question = new Question
        {
            Id = Guid.NewGuid(),
            QuestionContent = model.QuestionContent,
            QuestionImageUrl = null,
            Points = model.Points,
            Type = model.Type,
            TestId = model.TestId
        };

        await _unitOfWork.Questions.InsertAsync(question);
        var res = await _unitOfWork.SaveAsync(teacherId);

        return question.Id;
    }
    public async Task DeleteQuestionAsync(Guid teacherId, Guid questionId)
    {
        var question = await _unitOfWork.Questions.GetFirstOrDefaultAsync(x => x.Id == questionId);
        if (questionId == null)
        {
            throw new NotFoundException($"Question with id - {questionId} ");
        }
        var answers = await _unitOfWork.AnswerOptions.GetAsync(x => x.QuestionId == questionId);

        _unitOfWork.AnswerOptions.Delete(answers);
        _unitOfWork.Questions.Delete(question);

        await _unitOfWork.SaveAsync(teacherId);
    }
    public async Task<QuestionDTO> GetQuestionAsync(Guid id)
    {
        var question = await _unitOfWork.Questions.GetFirstOrDefaultAsync(
            x => x.Id == id,
            x => x.Include(x => x.Test).Include(x => x.Answers));

        return _mapper.Map<QuestionDTO>(question);
    }
    public async Task EditQuestionAsync(Guid teacherId, EditQuestionModel model)
    {
        var question = await _unitOfWork.Questions.GetFirstOrDefaultAsync(x => x.Id == model.Id);
        if (question == null)
        {
            throw new NotFoundException($"Question with id - {model.Id} ");
        }

        question.QuestionContent = model.QuestionContent;
        question.Points = model.Points;

        _unitOfWork.Questions.Update(question);

        await _unitOfWork.SaveAsync(teacherId);
    }
    public async Task<Guid> CreateAnswerAsync(Guid teacherId, AddAnswerModel model)
    {
        var answer = new AnswerOption
        {
            Id = Guid.NewGuid(),
            Content = model.Content,
            IsCorrect = model.IsCorrect,
            AnswerImageUrl = null,
            QuestionId = model.QuestionId
        };

        await _unitOfWork.AnswerOptions.InsertAsync(answer);
        var res = await _unitOfWork.SaveAsync(teacherId);

        return answer.Id;
    }
    public async Task DeleteAnswerAsync(Guid teacherId, Guid answerId)
    {
        var answer = await _unitOfWork.AnswerOptions.GetFirstOrDefaultAsync(x => x.Id == answerId);
        if (answer == null)
        {
            throw new NotFoundException($"Answer with id - {answerId} ");
        }

        _unitOfWork.AnswerOptions.Delete(answer);

        await _unitOfWork.SaveAsync(teacherId);
    }
    public async Task<PassingTestDTO> StartTest(Guid studentId, Guid testId)
    {
        var test = await _unitOfWork.Tests.GetFirstOrDefaultAsync(
                    x => x.Id == testId,
                    x => x.Include(x => x.Teacher).Include(x => x.Questions).ThenInclude(x => x.Answers));

        if (test == null) throw new NotFoundException($"Test with id - {testId} ");
        if(!test.Questions.Any()) throw new NotFoundException($"Test doesn't have questions");

        if (test.StartTime > DateTimeOffset.UtcNow) 
            throw new BadRequestException($"Test starts at {test.StartTime.ToString("G", CultureInfo.GetCultureInfo("de-DE"))}");

        if (test.EndTime < DateTimeOffset.UtcNow) 
            throw new BadRequestException($"Test already ended");

        var passingTest = await _unitOfWork.PassingTests.GetFirstOrDefaultAsync(x => x.TestId == testId && x.StudentId == studentId);

        if(passingTest is not null && passingTest.StudentFinishedAt is not null)
            throw new BadRequestException($"You already passed this test");
        if (passingTest is not null && passingTest!.StudentStartedAt < DateTimeOffset.UtcNow.AddMinutes(-test.DurationMinutes))
            throw new BadRequestException($"Test time ended");



        if (passingTest == null)
        {
            passingTest = new PassingTest
            {
                TestId = testId,
                StudentId = studentId,
                StudentStartedAt = DateTimeOffset.Now
            };
            await _unitOfWork.PassingTests.InsertAsync(passingTest);
            await _unitOfWork.SaveAsync(studentId);
        }
        var res = _mapper.Map<PassingTestDTO>(test);
        var question1 = _mapper.Map<QuestionDTO>(test.Questions.FirstOrDefault());
        res.Id = passingTest.Id;
        res.StudentStartedAt = passingTest.StudentStartedAt;
        res.CurrentQuestion = question1;

        return res;
    }

    public async Task<PassingTestDTO> GetQuestionTest(Guid studentId, Guid testId, Guid questionId)
    {
        var test = await _unitOfWork.Tests.GetFirstOrDefaultAsync(
                    x => x.Id == testId,
                    x => x.Include(x => x.Teacher).Include(x => x.Questions).ThenInclude(x => x.Answers));

        if (test == null) throw new NotFoundException($"Test with id - {testId} ");
        if (!test.Questions.Any()) throw new NotFoundException($"Test doesn't have questions");

        if (test.EndTime < DateTimeOffset.UtcNow)
            throw new BadRequestException($"Test already ended");

        var passingTest = await _unitOfWork.PassingTests.GetFirstOrDefaultAsync(x => x.TestId == testId && x.StudentId == studentId);

        if (passingTest is not null && passingTest.StudentFinishedAt is not null)
            throw new BadRequestException($"You already passed this test");
        if (passingTest is not null && passingTest!.StudentStartedAt < DateTimeOffset.UtcNow.AddMinutes(-test.DurationMinutes))
            throw new BadRequestException($"Test time ended");

        var res = _mapper.Map<PassingTestDTO>(test);
        var question1 = _mapper.Map<QuestionDTO>(test.Questions.FirstOrDefault(x => x.Id == questionId));
        res.Id = passingTest.Id;
        res.StudentStartedAt = passingTest.StudentStartedAt;
        res.CurrentQuestion = question1;

        return res;
    }

}
