using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chronos.Entities;
using Chronos.Abstract;

namespace Chronos.Controllers
{
    public class InviteController : Controller
    {
        private IInviteRepository inviteRepository;
        private IMemberItemsRepository memberItemRepository;

        public InviteController(IInviteRepository inviteRepositoryParam, IMemberItemsRepository memberItemRepositoryParam)
        {
            inviteRepository = inviteRepositoryParam;
            memberItemRepository = memberItemRepositoryParam;
        }

        /// <summary>
        /// Invites a user to a group
        /// </summary>
        /// <param name="userId">Who to invite</param>
        /// <param name="groupId">What group they are invited to</param>
        /// <param name="sender">Who sent the invite</param>
        [HttpPost]
        public void InviteUser(int userId, int groupId, int sender)
        {
            var invite = new InviteItem
            {
                UserId = userId,
                GroupId = groupId,
                Sender = sender,
                IsActive = true
            };
            inviteRepository.Insert(invite);
            inviteRepository.Save();
        }

        /// <summary>
        /// Accepts and invite and makes a user a member
        /// of the group
        /// </summary>
        /// <param name="id">The id of the invite item</param>
        /// <param name="userid">The accepting user</param>
        /// <param name="groupid">What group the user is joining</param>
        [HttpPost]
        public void AcceptInvite(int id, int userid, int groupid)
        {
            var memberItem = new MemberItem
            {
                UserId = userid,
                GroupId = groupid
            };
            memberItemRepository.Insert(memberItem);
            inviteRepository.SetInactive(id);
        }

        /// <summary>
        /// Declines and invite by setting it as inactive
        /// </summary>
        /// <param name="id">The invite item's id</param>
        [HttpPost]
        public void DeclineInvite(int id)
        {
            inviteRepository.SetInactive(id);
        }
    }
}