using Chronos.Entities;
using System.Data.Entity;

namespace Chronos.Concrete
{
    public class ChronosContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<MemberItem> MemberItems { get; set; }
    }
}