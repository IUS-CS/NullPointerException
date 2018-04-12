using Chronos.Entities;
using Chronos.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Chronos.Concrete
{
    public class ChronosContext : IdentityDbContext<ApplicationUser>
    {
        public ChronosContext() : base("ChronosContext")
        {
            Database.SetInitializer(new Initializer());
        }
        public DbSet<User> Users { get; set; }
    }
}