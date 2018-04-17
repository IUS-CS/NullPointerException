using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronos.Entities;

namespace Chronos.Abstract
{
    /// <summary>
    /// Interface injected by DI container
    /// </summary>
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }
        void Insert(User user);
        void Save();
        User GetUserByUsername(string username);
        List<Group> GetUsersGroupsById(int id);
        List<User> SearchUser(string username);
        List<User> SearchUserInvite(string username, List<int> memberIds);
        User GetUserById(int id);
        string GetUsernameById(int id);
    }
}