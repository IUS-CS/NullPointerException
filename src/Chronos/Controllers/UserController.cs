using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chronos.Abstract;

namespace Chronos.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository userRepository;

        public UserController(IUserRepository userRepositoryParam)
        {
            userRepository = userRepositoryParam;
        }
        
        public ActionResult SearchUser(string username)
        {
            var matches = userRepository.SearchUser(username);
            return PartialView(matches);
        }
    }
}