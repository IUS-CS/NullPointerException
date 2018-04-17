using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chronos.Entities
{
    /// <summary>
    /// A todo list item in the database
    /// </summary>
    public class TodoItem
    {
        public int Id { get; set; } //Id of this TodoItem
        public int GroupId { get; set; } //What group it belings to
        public string Text { get; set; } //The text it contains
        public Group Group { get; set; } 
    }
}