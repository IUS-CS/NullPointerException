using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chronos.Entities;
using Chronos.Abstract;

namespace Chronos.Concrete
{
    public class TodoRepository: ITodoRepository
    {
        public ChronosContext context = new ChronosContext();

        public IEnumerable<TodoItem> Items
        {
            get { return context.TodoItems; }
        }

        public void Insert(TodoItem item)
        {
            context.TodoItems.Add(item);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public List<TodoItem> GetItemByGroupID(int id)
        {
            return context.TodoItems.Where(x => x.GroupId == id).ToList();
        }

        public void remove(int id)
        {
            System.Diagnostics.Debug.WriteLine(id);
            context.TodoItems.Remove(context.TodoItems.Where(x => x.Id == id).ToArray()[0]);
            Save();
        }
    }
}