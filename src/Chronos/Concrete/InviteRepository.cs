using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronos.Abstract;
using Chronos.Entities;

namespace Chronos.Concrete
{
    public class InviteRepository : IInviteRepository
    {
        private ChronosContext context = new ChronosContext();

        public IEnumerable<InviteItem> InviteItems { get { return context.InviteItems; } } 
        public void Insert(InviteItem invite)
        {
            context.InviteItems.Add(invite);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}