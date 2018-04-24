using System.Web.Mvc;
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
        
        /// <summary>
        /// Creates a group
        /// </summary>
        /// <param name="group">the grou pname to be created</param>
        /// <returns>a new view of the created group's page</returns>
        [HttpPost]
        public RedirectToRouteResult CreateGroup(Group group)
        {
            int newGroupId = groupRepository.CreateGroup(group.GroupName, (int) Session["CurrentUserId"]); 
            return RedirectToAction("Index", new RouteValueDictionary(
                new { controller = "Home", action = "Index", Id = newGroupId }));
        }

        /// <summary>
        /// Switches the group page the user is viewing
        /// </summary>
        /// <param name="group">The group to be switched to</param>
        /// <returns>a new page showing the new group</returns>
        public RedirectToRouteResult SwitchGroup(Group group)
        {
            return RedirectToAction("Index", new RouteValueDictionary(
                new { controller = "Home", action = "Index", Id = group.Id }));
        }
    }
}