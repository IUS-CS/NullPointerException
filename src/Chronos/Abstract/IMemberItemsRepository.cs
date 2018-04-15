﻿using Chronos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chronos.Abstract
{
    public interface IMemberItemsRepository
    {
        void Insert(MemberItem item);
        void Save();
    }
}