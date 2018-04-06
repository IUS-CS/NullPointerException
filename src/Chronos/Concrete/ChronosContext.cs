using Chronos.Entities;
using System.Data.Entity;

namespace Chronos.Concrete
{
    public class ChronosContext : DbContext
    {
        public  virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<MemberItem> MemberItems { get; set; }
        public virtual DbSet<TodoItem> TodoItems { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasMany(x => x.TodoList)
                .WithRequired(y => y.Group);
        }*/
    }
}