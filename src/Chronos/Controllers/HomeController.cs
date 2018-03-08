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


        public HomeController(IUserRepository userRepositoryParam)
        {
            this.userRepository = userRepositoryParam;
        }
        // GET: Home
        public ActionResult Index()
        {
            List<User> members = (List<User>) this.userRepository.Users;

            TodoList list = new TodoList {
                Items = new List<string>()
            };
            list.Items.Add("Do this");
            list.Items.Add("Do that");
           
            Calendar userCalendar = new Calendar();
            userCalendar.StartTime = DateTime.Today;
            userCalendar.EndTime = DateTime.Today.AddDays(7);

            GroupContentModel groupContent = new GroupContentModel();
            groupContent.TodoList = list;
            groupContent.Calendar = userCalendar;
            groupContent.Members = members;
            return View(groupContent);
        }
        public ViewResult Login() {
            return View();
        }
    }
}