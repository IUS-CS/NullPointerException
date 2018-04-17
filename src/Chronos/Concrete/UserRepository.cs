using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronos.Abstract;
using Chronos.Entities;

namespace Chronos.Concrete
{
    /// <summary>
    /// Concrete implementation for working with users in the database
    /// </summary>
    public class UserRepository : IUserRepository
    {
        public ChronosContext context = new ChronosContext();

        /// <summary>
        /// Contains the users in this repository
        /// </summary>
        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }

        /// <summary>
        /// Adds a user to this repository
        /// </summary>
        /// <param name="user"></param>
        public void Insert(User user)
        {
            context.Users.Add(user);
        }

        /// <summary>
        /// Saves changes to the database
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Gets a user by their username
        /// </summary>
        /// <param name="username">a username</param>
        /// <returns>the user that has the given username</returns>
        public User GetUserByUsername(string username)
        {
            return context.Users
                .Where(x => x.Username == username)
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets the groups a user is a part of
        /// </summary>
        /// <param name="id">a user id</param>
        /// <returns>the groups that the user having id is a part of</returns>
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

        /// <summary>
        /// Gets a list of users matching a search string
        /// </summary>
        /// <param name="username">The username being serached</param>
        /// <returns>The list of users that could match the search string</returns>
        public List<User> SearchUser(string username)
        {
            return context.Users
                .Where(x => x.Username.Contains(username))
                .ToList();
        }

        /// <summary>
        /// Gets a list of users matching a search string, filtered
        /// so users already in the group won't be returned
        /// </summary>
        /// <param name="username">the search string</param>
        /// <param name="memberIds">The list of members in the current group</param>
        /// <returns>A filtered list of users that could match the search string</returns>
        public List<User> SearchUserInvite(string username, List<int> memberIds)
        {
            return context.Users
                .Where(x => x.Username.Contains(username) && !memberIds.Contains(x.Id))
                .ToList();
        }

        /// <summary>
        /// Gets a user matching id
        /// </summary>
        /// <param name="id">a user id</param>
        /// <returns>A user matching id</returns>
        public User GetUserById(int id)
        {
            return context.Users
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets the username of a user given the id
        /// </summary>
        /// <param name="id">a user id</param>
        /// <returns>the username of a user matching id</returns>
        public string GetUsernameById(int id)
        {
            return context.Users
                .Where(x => x.Id == id)
                .Select(x => x.Username)
                .First();
        }
    }
}