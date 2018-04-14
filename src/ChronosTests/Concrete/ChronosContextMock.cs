using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Entities;

namespace ChronosTests.Concrete
{
    public class ChronosContextMock
    {
        public virtual List<User> Users { get; set; } = new List<User>
        {
            new User {Id = 1, Username = "TestUser"}
        };
        public virtual List<Group> Groups { get; set; } = new List<Group>
        {
            new Group {Id = 1, Creator = 1, GroupName = "TestGroup"}
        };
        public virtual List<MemberItem> MemberItems { get; set; } = new List<MemberItem>
        {
            new MemberItem { Id = 1, GroupId = 1, UserId = 1}
        };
        public virtual List<TodoItem> TodoItems { get; set; } = new List<TodoItem>
        {
            new TodoItem { Id = 1, GroupId = 1, Text = "Test Todo", Group = new Group()}
        };
    }
}
