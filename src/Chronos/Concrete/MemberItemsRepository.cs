using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronos.Abstract;
using Chronos.Entities;

namespace Chronos.Concrete
{
    public class MemberItemsRepository : IMemberItemsRepository
    {
        private ChronosContext context = new ChronosContext();

        public void Insert(MemberItem item)
        {
            context.MemberItems.Add(item);
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}