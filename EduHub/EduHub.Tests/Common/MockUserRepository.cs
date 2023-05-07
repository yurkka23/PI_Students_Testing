using EduHub.Domain.Entities;
using EduHub.Persistence.Abstractions;
using Moq;

namespace EduHub.Tests.Common
{
    internal class MockUserRepository
    {
        public static Mock<IRepositoryAsync<User>> GetMock()
        {
            var mock = new Mock<IRepositoryAsync<User>>();
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
                    AboutMe = null,
                    Email = "urakarabin12@gmail.com"
                },
                new()
                {
                    Id = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA156"),
                    UserName = "vika1",
                    FirstName = "vi",
                    LastName = "ka",
                    UserImgUrl = "AppFiles\\Images\\9e7d6d75-7369-44e6-8010-a4622d563e83.jpg",
                    RegisterTime = DateTimeOffset.Now,
                    AboutMe = null,
                    Email = "vika@gmail.com"
                },
                new()
                {
                    Id = Guid.Parse("9D4C5258-DD24-4FF7-87A0-463FB3BAA157"),
                    UserName = "igor1",
                    FirstName = "ig",
                    LastName = "or",
                    UserImgUrl = "AppFiles\\Images\\9e7d6d75-7369-44e6-8010-a4622d563e83.jpg",
                    RegisterTime = DateTimeOffset.Now,
                    AboutMe = null,
                    Email = "igor@gmail.com"
                }
            };
            var tcs = new TaskCompletionSource<User>();
            // tcsreq.SetResult(requests);

            //mock.Setup(m => m.GetAsync(null, null, null, 0, int.MaxValue)).Returns(() => tcsreq.Task);


            //mock.Setup(m => m.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>(), null)).Returns((Expression<Func<User, bool>> ex) => {
            //     var res = users.AsQueryable().FirstOrDefault(ex);
            //    //tcs.SetResult(res);
            //    //return tcs.Task;
            //    return Task.FromResult(res);
            //   // return res;
            //    });


            //  Expression<Func<TEntity, bool>>? filter
            //mock.Setup(m => m.GetFirstOrDefaultAsync(x => x.Email == It.IsAny<string>(), null)).ReturnsAsync((string email) => { 
            //    users.FirstOrDefault(o => o.Email == email)
            //    });

            return mock;
        }
    }
}