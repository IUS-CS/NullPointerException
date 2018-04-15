using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronos.Abstract;
using Chronos.Entities;

namespace Chronos.Concrete
{
    public class UserRepository : IUserRepository
    {
        public ChronosContext context = new ChronosContext();

        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }

        public void Insert(User user)
        {
            context.Users.Add(user);
        }

        public void Save()
        {
            context.SaveChanges();
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
                .Distinct()
                .ToList();
        }

        public List<User> SearchUser(string username)
        {
            return context.Users
                .Where(x => x.Username.Contains(username))
                .ToList();
        }

        public List<User> SearchUserInvite(string username, List<int> memberIds)
        {
            return context.Users
                .Where(x => x.Username.Contains(username) && !memberIds.Contains(x.Id))
                .ToList();
        }

        public User GetUserById(int id)
        {
            return context.Users
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public string GetUsernameById(int id)
        {
            return context.Users
                .Where(x => x.Id == id)
                .Select(x => x.Username)
                .First();
        }
    }
}