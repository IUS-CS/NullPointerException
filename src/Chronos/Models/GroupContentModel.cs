using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronos.Entities;

namespace Chronos.Models
{
    public class GroupContentModel
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<TodoItem> TodoList { get; set; }
        public Calendar Calendar { get; set; }
        public IEnumerable<User> Members { get; set; }

        public static implicit operator GroupContentModel(Group group)
        {
            return new GroupContentModel
            {
                GroupName = group.GroupName,
                Members = new List<User>(),
                Calendar = new Calendar(),
                TodoList = new List<TodoItem>()
            };
        }
    }
}