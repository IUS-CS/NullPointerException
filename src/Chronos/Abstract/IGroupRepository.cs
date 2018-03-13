using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Entities;

namespace Chronos.Abstract
{
    interface IGroupRepository
    {
        Group GetFirstUserGroupById(int id);
    }
}
