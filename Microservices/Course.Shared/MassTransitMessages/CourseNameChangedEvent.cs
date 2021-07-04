using System;
using System.Collections.Generic;
using System.Text;

namespace Course.Shared.MassTransitMessages
{
    public class CourseNameChangedEvent
    {
        public string CourseId { get; set; }
        public string UpdatedCourseName { get; set; }
    }
}
