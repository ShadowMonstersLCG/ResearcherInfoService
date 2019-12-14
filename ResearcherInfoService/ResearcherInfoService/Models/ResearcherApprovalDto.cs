using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearcherInfoService.Models
{
    public class ResearcherApprovalDto
    {
        public int ProjectId { get; set; }
        public int ResearcherAvailabilityId { get; set; }
        public int ApprovalStatusId { get; set; }
        public string InfoRequested { get; set; }
        public int ResearcherId { get; set; }
        public string ApprovalStatus { get; set; }
        public string ProjectName { get; set; }
        public ResearcherAvailabilityDto ResearcherAvailability { get; set; }
    }
}