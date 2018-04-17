using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Chronos.Entities;
using Chronos.Concrete;

namespace Chronos.Concrete
{
    public class Initializer : DropCreateDatabaseIfModelChanges<ChronosContext>
    {
        /// <summary>
        /// Initializes the database if it doesn't exists
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(ChronosContext context)
        {
            var users = new List<User>
            {
                new User{Username="Nathan"},
                new User{Username="Nathaniel"}
            };
            var groups = new List<Group>
            {
                new Group { Creator = 1, GroupName = "TestGroup" }
            };
            var memberItems = new List<MemberItem>
            {
                new MemberItem { GroupId = 1, UserId = 1}
            };
            var todoItems = new List<TodoItem>
            {
                new TodoItem { GroupId = 1, Text = "Test Todo", Group = new Group()}
            };
            var inviteItems = new List<InviteItem>
            {
                new InviteItem { GroupId = 1, UserId = 2, Sender = 1, IsActive = true}
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            foreach (var group in groups)
            {
                context.Groups.Add(group);
            }
            foreach (var item in memberItems)
            {
                context.MemberItems.Add(item);
            }
            foreach (var item in todoItems)
            {
                context.TodoItems.Add(item);
            }
            foreach (var invite in inviteItems)
            {
                context.InviteItems.Add(invite);
            }
            context.SaveChanges();

        }
    }
}