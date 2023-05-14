using EduHub.Domain.Entities;
using EduHub.Persistence.Abstractions;
using Moq;

namespace EduHub.Tests.Common;

public class MockTeacherRequestRepository
{
    public static Mock<IRepositoryAsync<TeacherRequest>> GetMock()
    {
        var requests = new List<TeacherRequest>
        {
            new()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                Text = "text1 I'm teacher",
                FullName = "yurii karbain",
                ProofImage = "AppFiles\\Images\\9e7d6d75-7369-44e6-8010-a4622d563e83.jpg",
                UserId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA155"),
                CreatedAt = DateTimeOffset.Now,
                CreatedBy = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA155")
            },
            new()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728951e"),
                Text = "text2 I'm teacher",
                FullName = "igor",
                ProofImage = "AppFiles\\Images\\9e7d6d75-7369-44e6-8010-a4622d563e83.jpg",
                UserId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA155"),
                CreatedAt = DateTimeOffset.Now,
                CreatedBy = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA155")
            },
            new()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728952e"),
                Text = "text3 I'm teacher",
                FullName = "vika",
                ProofImage = "AppFiles\\Images\\9e7d6d75-7369-44e6-8010-a4622d563e83.jpg",
                UserId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA155"),
                CreatedAt = DateTimeOffset.Now,
                CreatedBy = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA155")
            },
            new()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728953e"),
                Text = "text3 I'm teacher",
                FullName = "nastia",
                ProofImage = "AppFiles\\Images\\9e7d6d75-7369-44e6-8010-a4622d563e83.jpg",
                UserId = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA155"),
                CreatedAt = DateTimeOffset.Now,
                CreatedBy = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA155")
            }
        };
  
        var mock = new Mock<IRepositoryAsync<TeacherRequest>>();

        mock.Setup(m => m.GetAsync(null, null, null, 0, int.MaxValue)).ReturnsAsync(() => requests);
        //mock.Setup(m => m.InsertAsync(It.IsAny<TeacherRequest>())).Returns((TeacherRequest request) =>
        //{
        //    requests.Add(request);
        //    return true;
        //});
       

        return mock;
    }
}