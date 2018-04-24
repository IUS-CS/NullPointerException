using System.Collections.Generic;
using System.Linq;
using Chronos.Abstract;
using Chronos.Entities;

namespace Chronos.Concrete
{
    /// <summary>
    /// Concrete implementation for working with invites in the database
    /// </summary>
    public class InviteRepository : IInviteRepository
    {
        private ChronosContext context = new ChronosContext();

        /// <summary>
        /// The items in this repository
        /// </summary>
        public IEnumerable<InviteItem> InviteItems { get { return context.InviteItems; } } 

        /// <summary>
        /// Adds a new item to the invites
        /// </summary>
        /// <param name="invite"></param>
        public void Insert(InviteItem invite)
        {
            context.InviteItems.Add(invite);
        }

        /// <summary>
        /// Gets a user's pending invites
        /// </summary>
        /// <param name="id">a user id</param>
        /// <returns>a list of a user's pending invites</returns>
        public List<InviteItem> GetUserInvitesByUserId(int id)
        {
            return context.InviteItems
                .Where(x => x.UserId == id)
                .Where(x => x.IsActive)
                .ToList();
        }

        /// <summary>
        /// Save changes to the database
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        /// <summary>
        /// Sets an invite as inactive so a user won't continue to see it after
        /// it's been accepted or declined
        /// </summary>
        /// <param name="id"></param>
        public void SetInactive(int id)
        {
            context.InviteItems.Find(id)
                .IsActive = false;

            Save();
        }
    }
}