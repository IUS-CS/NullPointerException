using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace Chronos.Models
{
    public class CalendarModel
    {   
        [Required]
        public DateTime startTime { get; set; }
        [Required]
        public DateTime endTime { get; set; }
        [Required]
        public List<string> TimesBusy { get; set; }
    }
}