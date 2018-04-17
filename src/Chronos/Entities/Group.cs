using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chronos.Entities
{
    /// <summary>
    /// A group entity in the database
    /// </summary>
    public class Group
    {
        public int Id { get; set; } //Group's id
        public string GroupName { get; set; } //Name of the group
        public int Creator { get; set; } //Who made the group
    }
}