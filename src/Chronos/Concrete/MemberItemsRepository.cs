using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronos.Abstract;
using Chronos.Entities;

namespace Chronos.Concrete
{
    /// <summary>
    /// Concrete implementation for working with member items in the database
    /// </summary>
    public class MemberItemsRepository : IMemberItemsRepository
    {
        private ChronosContext context = new ChronosContext();

        /// <summary>
        /// Adds a new member item to this repository
        /// </summary>
        /// <param name="item"></param>
        public void Insert(MemberItem item)
        {
            context.MemberItems.Add(item);
            Save();
        }

        /// <summary>
        /// Saves changes to the database
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }
    }
}