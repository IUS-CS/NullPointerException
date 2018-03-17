using Chronos.Entities;
using System.Data.Entity;

namespace Chronos.Concrete
{
    public class ChronosContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<MemberItem> MemberItems { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasMany(x => x.TodoList)
                .WithRequired(y => y.Group);
        }*/
    }
}