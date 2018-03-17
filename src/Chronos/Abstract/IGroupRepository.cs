using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Entities;
using Chronos.Models;

namespace Chronos.Abstract
{
    public interface IGroupRepository
    {
        GroupContentModel GetGroupById(int id);
        Group GetFirstUserGroupById(int id);
    }
}
