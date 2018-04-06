using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chronos.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public int Creator { get; set; }
        public string Text { get; set; }
        public int GroupId { get; set; }
    }
}