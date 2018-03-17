using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chronos.Concrete;
using Chronos.Abstract;
using Chronos.Entities;
using System.Web.Routing;

namespace Chronos.Controllers
{
    public class GroupController : Controller
    {
        IGroupRepository groupRepository;
        public GroupController(IGroupRepository groupRepositoryParam)
        {
            groupRepository = groupRepositoryParam;
        }
        
        [HttpPost]
        public RedirectToRouteResult CreateGroup(Group group)
        {
            int newGroupId = groupRepository.CreateGroup(group.GroupName, 1); //TODO: user id is hard coded, needs to come from session
            return RedirectToAction("Index", new RouteValueDictionary(
                new { controller = "Home", action = "Index", Id = newGroupId }));
        }
    }
}