using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearcherInfoService.Models
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public bool Approved { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public Nullable<bool> IsPublished { get; set; }
        public string State { get; set; }
        public string Status {get; set;}
        public string InfoRequested { get; set; }
    }
}