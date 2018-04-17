using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chronos.Entities
{
    /// <summary>
    /// Database object connecting a user to a group
    /// </summary>
    public class MemberItem
    {
        public int Id { get; set; } //Id of this member item
        public int UserId { get; set; } //The user concerned
        public int GroupId { get; set; } //The group concerned
    }
}