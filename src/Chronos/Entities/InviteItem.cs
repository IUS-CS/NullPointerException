using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chronos.Entities
{
    /// <summary>
    /// An item linking a user and a group they've been invited to
    /// </summary>
    public class InviteItem
    {
        public int Id { get; set; } //Id of this invite
        public int UserId { get; set; } //who was invited
        public int GroupId { get; set; } //Which group they were invited to
        public int Sender { get; set; } //Who sent the invite
        public bool IsActive { get; set; } //Whether or not this invite is to e displayed
    }
}