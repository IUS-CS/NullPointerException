using Chronos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chronos.Abstract;
using Chronos.Entities;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Chronos.Concrete;

namespace Chronos.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository userRepository;
        private IGroupRepository groupRepository;

        public HomeController(IUserRepository userRepositoryParam, IGroupRepository groupRepositoryParam)
        {
            userRepository = userRepositoryParam;
            groupRepository = groupRepositoryParam;
        }
        // GET: Home
        public  ActionResult Index()
        {
            var task = HttpContext.GetOwinContext().Authentication.GetExternalIdentityAsync(
                DefaultAuthenticationTypes.ApplicationCookie);
            var userName = task.Result.GetUserName();

            var user = userRepository.GetUserByUsername(userName);
            if (user == null)
            {
                userRepository.Insert(new User { Username = userName });
                userRepository.Save();
                user = userRepository.GetUserByUsername(userName);
            }
            Session["CurrentUser"] = user;
            Session["CurrentUserId"] = user.Id;

            var groups = userRepository.GetUsersGroupsById(user.Id);
            Session["UserGroups"] = groups;
            var groupId = groups[0].Id;
            var group = groupRepository.GetGroupById(Int32.Parse(groupId.ToString()));
            return View(group);
        }
    }
}