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

        [HttpPost]
        public void DeclineInvite(int id)
        {
            inviteRepository.SetInactive(id);
        }
    }
}