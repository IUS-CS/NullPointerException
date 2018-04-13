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
    public class IGroupRepositoryMock : IGroupRepository
    {
        private ChronosContextMock mContext;

        public IGroupRepositoryMock(ChronosContextMock mContextParam)
        {
            mContext = mContextParam;
        }

        public GroupContentModel GetGroupById(int id)
        {
            var todoItems = GetTodoItemsByGroupId(id);
            var members = GetMembersByGroupId(id);
            var group = mContext.Groups
                .Where(x => x.Id == id)
                .Select(x => new { Id = x.Id, GroupName = x.GroupName, Creator = x.Creator })
                .FirstOrDefault();

            return new GroupContentModel
            {
                GroupName = group.GroupName,
                TodoList = todoItems,
                Calendar = new Calendar(),
                Members = members
            };
        }
        public List<TodoItem> GetTodoItemsByGroupId(int id)
        {
            return mContext.TodoItems
                .Where(x => x.GroupId == id)
                .ToList();
        }
        public List<User> GetMembersByGroupId(int id)
        {
            return mContext.MemberItems
                .Where(x => x.GroupId == id)
                .Join(mContext.Users,
                x => x.UserId,
                y => y.Id,
                (x, y) => new { User = y })
                .Select(x => x.User)
                .ToList();
        }
        public Group GetFirstUserGroupById(int id)
        {
            return mContext.Groups
                .Join(
                    mContext.MemberItems,
                    x => x.Id,
                    y => y.GroupId,
                    (x, y) => new { Group = x, MemberItem = y }
                )
                .Where(x => x.MemberItem.UserId == id)
                .Select(x => x.Group)
                .FirstOrDefault();
        }

        public int CreateGroup(string name, int userId)
        {
            mContext.Groups.Add(new Group { GroupName = name, Creator = userId, Id = 2 });
            var groupId = GetGroupIdByGroupName(name);
            mContext.MemberItems.Add(new MemberItem { UserId = userId, GroupId = groupId });
            return groupId;
        }

        public int GetGroupIdByGroupName(string name)
        {
            return mContext.Groups
                .Where(x => x.GroupName == name)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        public void Remove()
        {
            mContext.Groups.Remove(new Group { Id = 2, Creator = 1, GroupName = "TestGroup2" });
        }

        public void Save()
        {
            //Needed to satisfy interface
        }
    }

    [TestClass]
    public class GroupRepositoryTests
    {
        private IGroupRepositoryMock groupRepo;

        [TestInitialize]
        public void Initialize()
        {
            groupRepo = new IGroupRepositoryMock(new ChronosContextMock());
        }

        [TestMethod]
        public void GetGroupByIdReturnsCorrectGroup()
        {
            //Arrange
            int targetId = 1;

            //Act
            var group = groupRepo.GetGroupById(targetId);

            //Assert
            Assert.AreEqual(group.GroupName, "TestGroup");
        }
        
        [TestMethod]
        public void GetTodoItemsByIdReturnsCorrectItems()
        {
            //Arrange
            int targetId = 1;

            //Act
            var items = groupRepo.GetTodoItemsByGroupId(targetId);

            //Assert
            Assert.AreEqual(items[0].Text, "Test Todo");
        }

        [TestMethod]
        public void GetFirstUserGroupByIdReturnsCorrectGroup()
        {
            //Arrange
            int targetId = 1;

            //Act
            var group = groupRepo.GetFirstUserGroupById(targetId);

            //Assert
            Assert.AreEqual(group.GroupName, "TestGroup");
        }

        [TestMethod]
        public void GetGroupIdByGroupNameReturnsCorrectId()
        {
            //Arrange
            string groupName = "TestGroup";

            //Act
            var id = groupRepo.GetGroupIdByGroupName(groupName);

            //Assert
            Assert.AreEqual(id, 1);
        }

        [TestMethod]
        public void GetMembersByGroupIdReturnsCorrectUsers()
        {
            //Arrange
            int targetId = 1;

            //Act
            var members = groupRepo.GetMembersByGroupId(targetId);

            //Assert
            Assert.AreEqual(members[0].Id, 1);
        }

        [TestMethod]
        public void CreateGroupCreatesANewGroup()
        {
            //Arrange
            int creatorId = 1;
            string targetGroupName = "TestGroup2";

            //Act
            var newGroupId = groupRepo.CreateGroup(targetGroupName, creatorId);

            //Assert
            Assert.AreEqual(newGroupId, 2);

            //Cleanup
            groupRepo.Remove();
        }

    }
}
