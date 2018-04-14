using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chronos.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int Creator { get; set; }
    }
}