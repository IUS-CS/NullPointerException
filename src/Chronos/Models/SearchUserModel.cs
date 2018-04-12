using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronos.Entities;

namespace Chronos.Models
{
    public class SearchUserModel
    {
        public List<User> Users { get; set; } = new List<User>();
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }
}