using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronos.Abstract;
using Chronos.Entities;
using Chronos.Models;

namespace Chronos.Concrete
{
    /// <summary>
    /// Concrete implementation for working with groups in the database
    /// </summary>
    public class GroupRepository : IGroupRepository
    {
        private ChronosContext context = new ChronosContext();

        public GroupRepository(ChronosContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets a group by the id
        /// </summary>
        /// <param name="id">di of the requested group</param>
        /// <returns>the group matching id</returns>
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
                Id = id,
                GroupName = group.GroupName,
                TodoList = todoItems,
                //Calendar = new Calendar(),
                Members = members
            };
        }

        /// <summary>
        /// Gets the todo list of a group
        /// </summary>
        /// <param name="id">a group id</param>
        /// <returns>The todolist of the group matching id</returns>
        private List<TodoItem> GetTodoItemsByGroupId(int id)
        {
            return context.TodoItems
                .Where(x => x.GroupId == id)
                .ToList();
        }

        /// <summary>
        /// Gets the members of a group
        /// </summary>
        /// <param name="id">a group id</param>
        /// <returns>the users in the group matching id</returns>
        public List<User> GetMembersByGroupId(int id)
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
        
        /// <summary>
        /// Get the group to display to theh user on login
        /// </summary>
        /// <param name="id">a user id</param>
        /// <returns>the first group this user is a part of</returns>
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

        /// <summary>
        /// Creates a new group and adds the creator as a member
        /// </summary>
        /// <param name="name">the group name</param>
        /// <param name="userId">the current user's id</param>
        /// <returns>the id of the new group</returns>
        public int CreateGroup(string name, int userId)
        {
            context.Groups.Add(new Group { GroupName = name, Creator = userId});
            Save();
            var groupId = GetGroupIdByGroupName(name);
            context.MemberItems.Add(new MemberItem { UserId = userId, GroupId = groupId });
            Save();
            return groupId;
        }

        /// <summary>
        /// Gets the id of a group
        /// </summary>
        /// <param name="name">the group name being searched</param>
        /// <returns>a group id</returns>
        public int GetGroupIdByGroupName(string name)
        {
            return context.Groups
                .Where(x => x.GroupName == name)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        /// <summary>
        /// Saves changes to the database
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Returns the name of a group
        /// </summary>
        /// <param name="id">a group id</param>
        /// <returns>the name of the group matching id</returns>
        public string GetGroupNameById(int id)
        {
            return context.Groups
                .Where(x => x.Id == id)
                .Select(x => x.GroupName)
                .First();
        }
    }
}