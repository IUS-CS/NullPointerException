﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chronos.Abstract;
using Chronos.Entities;
using Chronos.Models;

namespace Chronos.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository userRepository;
        private IGroupRepository groupRepository;

        public UserController(IUserRepository userRepositoryParam, IGroupRepository groupRepositoryParam)
        {
            userRepository = userRepositoryParam;
            groupRepository = groupRepositoryParam;
        }
        
        public ActionResult SearchUser(string username, int groupId)
        {
            var members = groupRepository.GetMembersByGroupId(groupId);
            List<int> memberIds = new List<int>();
            foreach (var member in members)
            {
                memberIds.Add(member.Id);
            }
            var matches = userRepository.SearchUserInvite(username, memberIds);
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