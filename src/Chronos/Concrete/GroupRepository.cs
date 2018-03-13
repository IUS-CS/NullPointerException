using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronos.Abstract;
using Chronos.Entities;

namespace Chronos.Concrete
{
    public class GroupRepository : IGroupRepository
    {
        private ChronosContext context = new ChronosContext();

        public Group GetFirstUserGroupById(int id)
        {
            return context.Groups
                .Join(
                    context.MemberItem
                    x => x.
                )
        }
    }
}