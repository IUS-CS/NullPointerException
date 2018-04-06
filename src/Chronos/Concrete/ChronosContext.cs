using Chronos.Entities;
using Chronos.Concrete;
using System.Data.Entity;

namespace Chronos.Concrete
{
    public class ChronosContext : DbContext
    {
        public ChronosContext() : base("ChronosContext")
        {
            Database.SetInitializer(new Initializer());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<TodoItem> Items { get; set; }
    }
}