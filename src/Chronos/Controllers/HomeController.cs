using Chronos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chronos.Abstract;
using Chronos.Entities;

namespace Chronos.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository userRepository;
        private IGroupRepository groupRepository;
        private ITodoRepository todoRepository;

        public HomeController(IUserRepository userRepositoryParam, IGroupRepository groupRepositoryParam)
        {
            userRepository = userRepositoryParam;
            groupRepository = groupRepositoryParam;
        }
        // GET: Home
        public ActionResult Index(User user)
        {
            Session["CurrentUserId"] = user.Id;
            ViewBag.UserGroups = userRepository.GetUsersGroupsById(user.Id);
            var groupId = RouteData.Values["id"];
            var group = groupRepository.GetGroupById(Int32.Parse(groupId.ToString()));
            return View(group);
        }

        [HttpPost]
        public RedirectToRouteResult Index(GroupContentModel model)
        {
            var item = new TodoItem();
           // item.Creator = ViewBag.User.UserName;
            item.GroupId = 0;
            item.Text = model.TodoList.AddItem;

            todoRepository.Insert(item);
            todoRepository.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public RedirectToRouteResult Remove(int Id)
        {
            todoRepository.remove(Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Login() {
            return View();
        }
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