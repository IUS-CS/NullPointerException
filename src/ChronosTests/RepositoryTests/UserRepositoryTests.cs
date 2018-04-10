using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chronos.Concrete;
using Chronos.Entities;
using Chronos.Models;
using Moq;
using System.Data.Entity;
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

        public ChronosContext context = new ChronosContext();

        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }

        public void Insert(User user)
        {
            context.Users.Add(user);
        }

        public User GetUserByUsername(string username)
        {
            return context.Users
                .Where(x => x.Username == username)
                .FirstOrDefault();
        }

        public List<Group> GetUsersGroupsById(int id)
        {
            return context.Users
                .Join(context.MemberItems,
                x => x.Id,
                y => y.UserId,
                (x, y) => new { MemberItem = y })
                .Join(context.Groups,
                x => x.MemberItem.GroupId,
                y => y.Id,
                (x, y) => new { Group = y })
                .Select(x => x.Group)
                .ToList();
        }

        public List<User> SearchUser(string username)
        {
            return context.Users
                .Where(x => x.Username.Contains(username))
                .ToList();
        }

        public void Save()
        {
            //Needed to satisfy interface
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
            var user = new User();
        }

    }
}
