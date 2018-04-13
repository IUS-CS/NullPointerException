﻿using Chronos.Models;
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
            var foo = userRepository;

            var user = userRepository.GetUserByUsername(userName);
            if (user == null)
            {
                userRepository.Insert(new User { Username = userName });
                userRepository.Save();
                user = userRepository.GetUserByUsername(userName);
            }
            
            ViewBag.UserGroups = userRepository.GetUsersGroupsById(user.Id);
            var groupId = RouteData.Values["id"];
            var group = groupRepository.GetGroupById(Int32.Parse(groupId.ToString()));
            return View(group);
        }
        /*
        [HttpGet]
        public ActionResult Login() {
            return View();
        }
        */
        [HttpPost]
        public RedirectToRouteResult Login(User user)
        {
            var result = userRepository.GetUserByUsername(user.Username);
            if (result == null)
            {
                userRepository.Insert(user);
                userRepository.Save();
            }
            result = userRepository.GetUserByUsername(user.Username);
            var group = groupRepository.GetFirstUserGroupById(result.Id);
            return RedirectToAction("Index", new { id = group.Id});
        }
    }
}