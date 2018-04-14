using Chronos.Abstract;
using Chronos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chronos.Controllers
{
    public class ProfileController : Controller
    {
        private IUserRepository userRepository;
        private IInviteRepository inviteRepository;

        public ProfileController(IUserRepository userRepositoryParam, IInviteRepository inviteRepositoryParam)
        {
            userRepository = userRepositoryParam;
            inviteRepository = inviteRepositoryParam;
        }

        // GET: Profile
        public ActionResult ProfilePage()
        {
            int userId = (int) Session["CurrentUserId"];
            var groups = userRepository.GetUsersGroupsById(userId);
            var invites = inviteRepository.GetUserInvitesByUserId(userId);
            ProfilePageModel model = new ProfilePageModel
            {
                Groups = groups,
                Invites = invites
            };
            return View(model);
        }
    }
}