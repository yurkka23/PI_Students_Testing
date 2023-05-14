using EduHub.Domain.Constants;
using EduHub.Domain.Entities;
using EduHub.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq;
using System.Linq.Expressions;

namespace EduHub.Tests.Common;

public class MockUserRepository
{
    public static Mock<IRepositoryAsync<User>> GetMock()
    {
        var users = new List<User>
        {
            new()
            {
                Id = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA155"),
                UserName = "yurka23",
                FirstName = "yu",
                LastName = "ka",
                UserImgUrl = "AppFiles\\Images\\9e7d6d75-7369-44e6-8010-a4622d563e83.jpg",
                RegisterTime = DateTimeOffset.Now,
                AboutMe = RoleConstants.StudentRole,
                Email = "urakarabin12@gmail.com",
            },
            new()
            {
                Id = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA158"),
                UserName = "igor",
                FirstName = "aaa",
                LastName = "aaa",
                UserImgUrl = "AppFiles\\Images\\9e7d6d75-7369-44e6-8010-a4622d563e83.jpg",
                RegisterTime = DateTimeOffset.Now,
                AboutMe = RoleConstants.StudentRole,
                Email = "igor56@gmail.com",
            },
             new()
            {
                Id = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA159"),
                UserName = "igor45",
                FirstName = "aaa",
                LastName = "aaa",
                UserImgUrl = "AppFiles\\Images\\9e7d6d75-7369-44e6-8010-a4622d563e83.jpg",
                RegisterTime = DateTimeOffset.Now,
                AboutMe = RoleConstants.StudentRole,
                Email = "igor569@gmail.com",
            },
              new()
            {
                Id = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA160"),
                UserName = "igor78",
                FirstName = "aaa",
                LastName = "aaa",
                UserImgUrl = "AppFiles\\Images\\9e7d6d75-7369-44e6-8010-a4622d563e83.jpg",
                RegisterTime = DateTimeOffset.Now,
                AboutMe = RoleConstants.StudentRole,
                Email = "igor5699@gmail.com",
            },
            new()
            {
                Id = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA156"),
                UserName = "vika1",
                FirstName = "vi",
                LastName = "ka",
                UserImgUrl = "AppFiles\\Images\\9e7d6d75-7369-44e6-8010-a4622d563e83.jpg",
                RegisterTime = DateTimeOffset.Now,
                AboutMe = RoleConstants.TeacherRole,
                Email = "vika@gmail.com"
            },
            new()
            {
                Id = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA162"),
                UserName = "vika6",
                FirstName = "vi",
                LastName = "ka",
                UserImgUrl = "AppFiles\\Images\\9e7d6d75-7369-44e6-8010-a4622d563e83.jpg",
                RegisterTime = DateTimeOffset.Now,
                AboutMe = RoleConstants.TeacherRole,
                Email = "vika900@gmail.com"
            },
            new()
            {
                Id = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA256"),
                UserName = "vika1333",
                FirstName = "vi",
                LastName = "ka",
                UserImgUrl = "AppFiles\\Images\\9e7d6d75-7369-44e6-8010-a4622d563e83.jpg",
                RegisterTime = DateTimeOffset.Now,
                AboutMe = RoleConstants.TeacherRole,
                Email = "vika223@gmail.com"
            },
            new()
            {
                Id = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA157"),
                UserName = "igor1",
                FirstName = "ig",
                LastName = "or",
                UserImgUrl = "AppFiles\\Images\\9e7d6d75-7369-44e6-8010-a4622d563e83.jpg",
                RegisterTime = DateTimeOffset.Now,
                AboutMe = RoleConstants.AdminRole,
                Email = "igor@gmail.com"
            }
        };

        var userRepositoryMock = new Mock<IRepositoryAsync<User>>();

        userRepositoryMock.Setup(x => x.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>())).ReturnsAsync((Expression<Func<User, bool>> exp, object nul) =>
        {
            var res = users.AsQueryable().FirstOrDefault(exp);
            return res;
        });

        return userRepositoryMock;
    }
}