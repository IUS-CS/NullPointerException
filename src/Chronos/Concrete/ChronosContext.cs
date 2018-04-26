using Chronos.Entities;
using System.Data.Entity;

namespace Chronos.Concrete
{
    /// <summary>
    /// The database context for the application
    /// </summary>
    public class ChronosContext : DbContext
    {
        /// <summary>
        /// Sets the initializer for the database
        /// </summary>
        public ChronosContext() : base("ChronosContext")
        {
            Database.SetInitializer(new Initializer());
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<MemberItem> MemberItems { get; set; }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<InviteItem> InviteItems { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasMany(x => x.TodoList)
                .WithRequired(y => y.Group);
        }*/
    }
}