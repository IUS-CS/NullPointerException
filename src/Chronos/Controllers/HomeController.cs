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
        private ITodoRepository todoRepository;

        public HomeController(IUserRepository userRepositoryParam, ITodoRepository todoRepositoryParam)
        {
            this.userRepository = userRepositoryParam;
            this.todoRepository = todoRepositoryParam;
        }
        
        public ActionResult Index(User user)
        {

            ViewBag.User = user;
            var members = this.userRepository.Users;

            var todoItems = this.todoRepository.GetItemByGroupID(0);
            TodoList list = new TodoList {
                Items = new List<TodoItem>()
            };
            for (var i = 0; i < todoItems.Count; i++)
            {
                list.Items.Add(todoItems[i]);
            }

            Calendar userCalendar = new Calendar();
            userCalendar.StartTime = DateTime.Today;
            userCalendar.EndTime = DateTime.Today.AddDays(7);

            GroupContentModel groupContent = new GroupContentModel();
            groupContent.TodoList = list;
            groupContent.Calendar = userCalendar;
            groupContent.Members = members;
            return View(groupContent);
        }

        [HttpPost]
        public RedirectToRouteResult Index(GroupContentModel model)
        {
            var item = new TodoItem
            {
                Creator = ViewBag.User.UserName,
                GroupId = 0,
                Text = model.TodoList.AddItem,
            };
            todoRepository.Insert(item);
            todoRepository.Save();
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
            return RedirectToAction("Index");
        }
    }
}