using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chronos.Abstract;
using Chronos.Models;

namespace Chronos.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository userRepository;

        public UserController(IUserRepository userRepositoryParam)
        {
            userRepository = userRepositoryParam;
        }
        
        public ActionResult SearchUser(string username, int groupId)
        {
            var matches = userRepository.SearchUser(username);
            var model = new SearchUserModel
            {
                Users = matches,
                GroupId = groupId,
                UserId = Int32.Parse(Session["CurrentUserId"].ToString())
            };
            return PartialView(model);
        }
    }
}