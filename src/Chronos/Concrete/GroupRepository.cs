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
            var group = context.Groups
                .Where(x => x.Id == id)
                .Select(x => new { Id = x.Id, GroupName = x.GroupName, Creator = x.Creator })
                .FirstOrDefault();

            return new GroupContentModel
            {
                GroupName = group.GroupName,
                TodoList = todoItems,
                Calendar = new Calendar(),
                Members = new List<User>()
            };
        }
        private List<TodoItem> GetTodoItemsByGroupId(int id)
        {
            return context.TodoItems
                .Where(x => x.GroupId == id)
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
            Save();
            return GetGroupIdByGroupName(name);
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