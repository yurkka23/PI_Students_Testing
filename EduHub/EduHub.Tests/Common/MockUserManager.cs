using EduHub.Domain.Constants;
using EduHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace EduHub.Tests.Common;

internal class MockUserManager
{
    public static Mock<UserManager<User>> GetMock()
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


        var UserStoreMock = Mock.Of<IUserStore<User>>();
        var userMgr = new Mock<UserManager<User>>(UserStoreMock, null, null, null, null, null, null, null, null);

        userMgr.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((string id) =>
        {
            var res = users.FirstOrDefault(x => x.Id == Guid.Parse(id));
            return res; 
        });

        userMgr.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((string name) =>
        {
            var res = users.FirstOrDefault(x => x.UserName == name);
            return res;
        });
        userMgr.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((string email) =>
        {
            var res = users.FirstOrDefault(x => x.Email == email);
            return res;
        });

        userMgr.Setup(x => x.GetUsersInRoleAsync(It.IsAny<string>())).ReturnsAsync((string role) =>
        {
            return users.Where(x => x.AboutMe == role).ToList();
        });

        userMgr.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync((User user) =>
        {
            users.Remove(user);
            return IdentityResult.Success;
        });
        userMgr.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync((User user) =>
        {
            return IdentityResult.Success;
        });
        //UpdateAsync

        return userMgr;
    }
}