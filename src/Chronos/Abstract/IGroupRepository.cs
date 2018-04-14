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
        int GetGroupIdByGroupName(string name);
        Group GetFirstUserGroupById(int id);
        int CreateGroup(string name, int userId);
        void Save();
        List<User> GetMembersByGroupId(int id);
    }
}
