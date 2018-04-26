using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chronos.Entities;
using System.Collections.Generic;
using System.Linq;
using Chronos.Abstract;
using ChronosTests.Concrete;

namespace ChronosTests.RepositoryTests
{
    public class IUserRepositoryMock : IUserRepository
    {
        private ChronosContextMock mContext;

        public IUserRepositoryMock(ChronosContextMock mContextParam)
        {
            mContext = mContextParam;
        }

        public IEnumerable<User> Users
        {
            get { return mContext.Users; }
        }

        public void Insert(User user)
        {
            mContext.Users.Add(user);
        }

        public User GetUserByUsername(string username)
        {
            return mContext.Users
                .Where(x => x.Username == username)
                .FirstOrDefault();
        }

        public List<Group> GetUsersGroupsById(int id)
        {
            return mContext.Users
                .Join(mContext.MemberItems,
                x => x.Id,
                y => y.UserId,
                (x, y) => new { MemberItem = y })
                .Join(mContext.Groups,
                x => x.MemberItem.GroupId,
                y => y.Id,
                (x, y) => new { Group = y })
                .Select(x => x.Group)
                .ToList();
        }

        public List<User> SearchUser(string username)
        {
            return mContext.Users
                .Where(x => x.Username.Contains(username))
                .ToList();
        }

        public void Remove()
        {
            mContext.Users.Remove(new User { Id = 2, Username = "TestUser2" });
        }

        public void Save()
        {
            //Needed to satisfy interface
        }

        public string GetUsernameById(int id)
        {
            return mContext.Users
                .Where(x => x.Id == id)
                .Select(x => x.Username)
                .First();
        }

        public User GetUserById(int id)
        {
            return mContext.Users
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public List<User> SearchUserInvite(string username, List<int> memberIds)
        {
            return mContext.Users
                .Where(x => x.Username.Contains(username) && !memberIds.Contains(x.Id))
                .ToList();
        }
    }

    [TestClass]
    public class UserRepositoryTests
    {
        private IUserRepositoryMock userRepo;

        [TestInitialize]
        public void Initialize()
        {
            userRepo = new IUserRepositoryMock(new ChronosContextMock());
        }

        [TestMethod]
        public void InsertAddsUser()
        {
            //Arrange
            var user = new User()
            {
                Id = 2,
                Username = "TestUser2"
            };

            //Act
            userRepo.Insert(user);

            //Assert
            Assert.IsTrue(userRepo.Users.Count() == 2);

            //Cleanup
            userRepo.Remove();
        }

        [TestMethod]
        public void GetUserByUsernameGetsCorrectUser()
        {
            //Arrange
            var targetUsername = "TestUser";

            //Act
            var resultUser = userRepo.GetUserByUsername(targetUsername);

            //Assert
            Assert.AreEqual(resultUser.Username, targetUsername);
        }

        [TestMethod]
        public void GetUsersGroupsByIdReturnsCorrectGroups()
        {
            //Arrange
            var targetId = 1;

            //Act
            var groups = userRepo.GetUsersGroupsById(targetId);

            //Assert
            Assert.AreEqual(groups[0].GroupName, "TestGroup");
        }

        [TestMethod]
        public void SearchuserReturnsCorrectUser()
        {
            //Arrange
            var searchString = "Test";

            //Act
            var user = userRepo.SearchUser(searchString);

            //Assert
            Assert.AreEqual(user[0].Username, "TestUser");
        }

        [TestMethod]
        public void SearchuserReturnsCorrectUsers()
        {
            //Arrange
            userRepo.Insert(new User { Id = 2, Username = "TestUser2" });
            var searchString = "Test";

            //Act
            var users = userRepo.SearchUser(searchString);

            //Assert
            foreach(var user in users)
            {
                Assert.IsTrue(userRepo.Users.Contains(user));
            }
        }

        [TestMethod]
        public void GetUsernameByIdReturnsCorrectUsername()
        {
            //Arrange
            var id = 1;

            //Act
            var result = userRepo.GetUsernameById(id);

            //Assert
            Assert.IsTrue(result.Equals("TestUser"));
        }

        [TestMethod]
        public void GetUserByIdReturnsCorrectUser()
        {
            //Arrange
            var id = 1;

            //Act
            var result = userRepo.GetUserById(id);

            //Assert
            Assert.IsTrue(result.Username.Equals("TestUser"));

        }

        [TestMethod]
        public void SearchUserInviteReturnsCorrectUsers()
        {
            //Arrange
            userRepo.Insert(new User { Id = 2, Username = "TestUser2" });
            var username = "TestUser";
            var memberIds = new List<int>
            {
                1
            };

            //Act
            var result = userRepo.SearchUserInvite(username, memberIds);

            //Assert
            Assert.IsTrue(result[0].Username.Equals("TestUser2"));

        }
    }
}
