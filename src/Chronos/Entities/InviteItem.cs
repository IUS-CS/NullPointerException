using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chronos.Entities
{
    public class InviteItem
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int Sender { get; set; }
        public bool IsActive { get; set; }
    }
}