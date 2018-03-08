using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronos.Abstract;
using Chronos.Entities;

namespace Chronos.Concrete
{
    public class UserRepository : IUserRepository
    {
        public ChronosContext context = new ChronosContext();

        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }
    }
}