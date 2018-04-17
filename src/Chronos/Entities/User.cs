using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chronos.Entities
{
    /// <summary>
    /// A user entity in the database
    /// </summary>
    public class User
    {
        public int Id { get; set; } //This user's id
        public string Username { get; set; } //This user's username
   
    }
}