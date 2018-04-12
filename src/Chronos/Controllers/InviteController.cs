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

        public InviteController(IInviteRepository inviteRepositoryParam)
        {
            inviteRepository = inviteRepositoryParam;
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
    }
}