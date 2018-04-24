using Chronos.Abstract;
using Chronos.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Chronos.Entities;

namespace Chronos.Controllers
{
    public class ProfileController : Controller
    {
        private IUserRepository userRepository;
        private IInviteRepository inviteRepository;
        private IGroupRepository groupRepository;

        public ProfileController(IUserRepository userRepositoryParam, IInviteRepository inviteRepositoryParam, 
            IGroupRepository groupRepositoryParam)
        {
            userRepository = userRepositoryParam;
            inviteRepository = inviteRepositoryParam;
            groupRepository = groupRepositoryParam;
        }

        // GET: Profile
        public ActionResult ProfilePage()
        {
            int userId = (int) Session["CurrentUserId"];
            var groups = userRepository.GetUsersGroupsById(userId);
            var invites = inviteRepository.GetUserInvitesByUserId(userId);
            var inviteTuples = new List<Tuple<InviteItem, string, string>>();
            foreach(var invite in invites)
            {
                inviteTuples.Add(new Tuple<InviteItem, string, string>(invite, groupRepository.GetGroupNameById(invite.GroupId), userRepository.GetUsernameById(invite.Sender)));
            }
            ProfilePageModel model = new ProfilePageModel
            {
                Groups = groups,
                Invites = inviteTuples
            };
            return View(model);
        }
    }
}