using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronos.Entities;

namespace Chronos.Models
{
    public class TodoList
    {
        public List<TodoItem> Items { get; set; } = new List<TodoItem>();

        public string AddItem { get; set; }

        public TodoItem addItem(TodoItem item)
        {
            Items.Add(item);
            return item;
        }
        public TodoItem reomoveItem(TodoItem item)
        {
            Items.Remove(item);
            return item;
        }
    }
}