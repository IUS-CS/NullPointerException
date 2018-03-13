using Chronos.Entities;
using System.Data.Entity;

namespace Chronos.Concrete
{
    public class ChronosContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}