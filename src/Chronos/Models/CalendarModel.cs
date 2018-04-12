using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace Chronos.Models
{
    public class CalendarModel
    {   
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required]
        public List<string> TimesBusy { get; set; }



        public CalendarModel ()
        {
            StartTime = DateTime.UtcNow;
            EndTime = StartTime.AddDays(7);
        }
    }
}