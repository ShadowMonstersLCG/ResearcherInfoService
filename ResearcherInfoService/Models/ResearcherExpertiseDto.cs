using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearcherInfoService.Models
{
    public class ResearcherExpertiseDto
    {
        public int ResearchExpertiseId { get; set; }
        public string AffiliatedOrgName { get; set; }
        public int ResearcherId { get; set; }
        public int ExpertiseId { get; set; }

        public ExpertiseDto Expertise { get; set; }
        public UserDto Researcher { get; set; }
    }
}