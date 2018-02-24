using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronos.Entities;

namespace Chronos.Models
{
    public class GroupContentModel
    {
        public TodoList TodoList { get; set; }
        public Calendar Calendar { get; set; }
        public List<User> Members { get; set; }
    }
}