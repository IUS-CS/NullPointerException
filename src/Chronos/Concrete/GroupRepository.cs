using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronos.Abstract;
using Chronos.Entities;
using Chronos.Models;

namespace Chronos.Concrete
{
    public class GroupRepository : IGroupRepository
    {
        private ChronosContext context = new ChronosContext();

        public GroupContentModel GetGroupById(int id)
        {
            var todoItems = GetTodoItemsByGroupId(id);
            var members = GetMembersByGroupId(id);
            var group = context.Groups
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
        private List<TodoItem> GetTodoItemsByGroupId(int id)
        {
            return context.TodoItems
                .Where(x => x.GroupId == id)
                .ToList();
        }
        private List<User> GetMembersByGroupId(int id)
        {
            return context.MemberItems
                .Where(x => x.GroupId == id)
                .Join(context.Users,
                x => x.UserId,
                y => y.Id,
                (x, y) => new { User = y })
                .Select(x => x.User)
                .ToList();
        }
        public Group GetFirstUserGroupById(int id)
        {
            return context.Groups
                .Join(
                    context.MemberItems,
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
            context.Groups.Add(new Group { GroupName = name, Creator = userId});
            var groupId = GetGroupIdByGroupName(name);
            context.MemberItems.Add(new MemberItem { UserId = userId, GroupId = groupId });
            Save();
            return groupId;
        }

        public int GetGroupIdByGroupName(string name)
        {
            return context.Groups
                .Where(x => x.GroupName == name)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}