using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Chronos.Entities;
using Chronos.Concrete;

namespace Chronos.Concrete
{
    public class Initializer : DropCreateDatabaseIfModelChanges<ChronosContext>
    {
        protected override void Seed(ChronosContext context)
        {
            var users = new List<User>
            {
                new User{Username="Nathan"},
                new User{Username="Nathaniel"}
            };
            foreach(var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();

        }
    }
}