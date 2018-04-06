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
            get { return context.Items; }
        }

        public void Insert(TodoItem item)
        {
            context.Items.Add(item);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public List<TodoItem> GetItemByGroupID(int id)
        {
            return context.Items.Where(x => x.GroupId == id).ToList();
        }
    }
}