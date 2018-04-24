using System.Collections.Generic;
using Chronos.Entities;

namespace Chronos.Models
{
    /// <summary>
    /// Contains the result of a search including a list
    /// of users, the group being search from for invitation,
    /// and the user searching
    /// </summary>
    public class SearchUserModel
    {
        public List<User> Users { get; set; } = new List<User>();
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }
}