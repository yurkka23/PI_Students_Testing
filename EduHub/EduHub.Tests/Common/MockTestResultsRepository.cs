using EduHub.Domain.Constants;
using EduHub.Domain.Entities;
using EduHub.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq;
using System.Linq.Expressions;

namespace EduHub.Tests.Common;

public class MockTestResultsRepository
{
    public static Mock<IRepositoryAsync<TestResult>> GetMock()
    {
        var results = new List<TestResult>
        {
            new()
            {
                Id = Guid.Parse("8D4C5258-DD24-4FF7-87A0-463FB3BAA155"),
                StudentId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA155"),
                TestId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA155"),
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA155"),
                SumPoints = 1,
                PercentageCorrrectAnswers = 10
            },
            new()
            {
               Id = Guid.Parse("8D4C5258-DD24-4FF7-87A0-463FB3BAA157"),
                StudentId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA157"),
                TestId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA157"),
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA157"),
                SumPoints = 10,
                PercentageCorrrectAnswers = 30
            },
           new()
            {
                Id = Guid.Parse("8D4C5258-DD24-4FF7-87A0-463FB3BAA158"),
                StudentId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA158"),
                TestId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA158"),
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA158"),
                SumPoints = 11,
                PercentageCorrrectAnswers = 34
            },
            new()
            {
               Id = Guid.Parse("8D4C5258-DD24-4FF7-87A0-463FB3BAA159"),
                StudentId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA159"),
                TestId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA159"),
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA159"),
                SumPoints = 15,
                PercentageCorrrectAnswers = 17
            },
            new()
            {
               Id = Guid.Parse("8D4C5258-DD24-4FF7-87A0-463FB3BAA156"),
                StudentId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA156"),
                TestId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA156"),
                CreatedAt = DateTime.Now,
                CreatedBy = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA156"),
                SumPoints = 12,
                PercentageCorrrectAnswers = 4
            }
        };

        var resultRepositoryMock = new Mock<IRepositoryAsync<TestResult>>();

        resultRepositoryMock.Setup(x => x.GetAsync(
            It.IsAny<Expression<Func<TestResult, bool>>>(), 
            It.IsAny<Func<IQueryable<TestResult>, IOrderedQueryable<TestResult>>>(),
            It.IsAny<Func<IQueryable<TestResult>, IIncludableQueryable<TestResult, object>>>(),
            0,
            int.MaxValue))
        .ReturnsAsync((Expression<Func<TestResult, bool>> filter, 
            Func<IQueryable<TestResult>, IOrderedQueryable<TestResult>> order,
            Func<IQueryable<TestResult>, IIncludableQueryable<TestResult, object>> include,
            int skip,
            int take) =>
        {
            var res = results.AsQueryable().Where(filter);
            return res;
        });

        return resultRepositoryMock;
    }
}
