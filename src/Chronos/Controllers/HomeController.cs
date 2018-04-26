using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chronos.Abstract;
using Chronos.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Chronos.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository userRepository;
        private IGroupRepository groupRepository;

        /// <summary>
        /// Constructor that gets concrete repositories from
        /// the dependency injection container
        /// </summary>
        /// <param name="userRepositoryParam"></param>
        /// <param name="groupRepositoryParam"></param>
        public HomeController(IUserRepository userRepositoryParam, IGroupRepository groupRepositoryParam)
        {
            userRepository = userRepositoryParam;
            groupRepository = groupRepositoryParam;
        }
        // GET: Home
        /// <summary>
        /// Displays the home page
        /// </summary>
        /// <returns></returns>
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
            if (groups.Count() == 0)
            {
                return View("NewUserPage");
            }
            
            var groupId = RouteData.Values["id"] ?? groups[0].Id;
            var group = groupRepository.GetGroupById(Int32.Parse(groupId.ToString()));
            return View(group);
        }
        public ActionResult UserProfile()
        {
            return View();
        }
    }
}