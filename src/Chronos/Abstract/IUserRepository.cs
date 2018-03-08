using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronos.Entities;

namespace Chronos.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }
        void Insert(User user);
        void Save();
    }
}