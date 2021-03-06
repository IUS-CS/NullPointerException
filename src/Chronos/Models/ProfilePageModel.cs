﻿using Chronos.Entities;
using System;
using System.Collections.Generic;

namespace Chronos.Models
{
    public class ProfilePageModel
    {
        public List<Group> Groups { get; set; }
        public List<Tuple<InviteItem, string, string>> Invites { get; set; }
    }
}