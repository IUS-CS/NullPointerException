using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace Chronos.Models
{
    public class FindMeetingTimesModel
    {   
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        
        public IList<TimePeriod> TimesFree { get; set; }

        public FindMeetingTimesModel ()
        {
            StartTime = DateTime.Now;
            EndTime = StartTime.AddDays(7);
        }
        
    }
}