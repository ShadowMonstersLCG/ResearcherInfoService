using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearcherInfoService.Models
{
    public class ResearcherAvailabilityDto
    {
        public int AvailabilityId { get; set; }
        public int ResearcherId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
    }
}