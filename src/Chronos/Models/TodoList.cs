using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chronos.Models
{
    public class TodoList
    {
        public List<string> Items {get; set;}

        public string addItem(string item)
        {
            Items.Add(item);
            return item;
        }
    }
}