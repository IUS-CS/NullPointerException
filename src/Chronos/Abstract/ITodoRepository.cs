using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronos.Entities;

namespace Chronos.Abstract
{
    public interface ITodoRepository
    {
        void Insert(TodoItem item);
        void Save();
        List<TodoItem> GetItemByGroupID(int id);
        void remove(int id);
    }
}
