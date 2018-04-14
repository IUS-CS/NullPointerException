using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Entities;

namespace Chronos.Abstract
{
    public interface IInviteRepository
    {
        IEnumerable<InviteItem> InviteItems { get; }
        void Insert(InviteItem invite);
        List<InviteItem> GetUserInvitesByUserId(int id);
        void Save();
    }
}
