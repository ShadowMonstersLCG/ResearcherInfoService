using ResearcherInfoService.DataAccess;
using ResearcherInfoService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ResearcherInfoService.Controllers
{
    public class ExpertiseController : ApiController
    {
        public string HealthCheck()
        {
            return "thumbs up!";
        }

        [HttpGet]
        public List<ExpertiseDto> GetAllExpertise()
        {
            using (ScheduleExEntities ctx = new ScheduleExEntities())
            {
                var query = from e in ctx.Expertises select new ExpertiseDto() { ExpertiseId = e.ExpertiseId, ExpertiseName = e.ExpertiseName };
                return query.ToList();
            }
        }

        [HttpGet]
        public int AddResearcherExpertise(int researcherId, int expertiseId, string affilicatedOrgName)
        {
            using (ScheduleExEntities ctx = new ScheduleExEntities())
            {
                DataAccess.ResearcherExpertis re = new DataAccess.ResearcherExpertis();
                re.ResearcherId = researcherId;
                re.ExpertiseId = expertiseId;
                re.AffiliatedOrgName = affilicatedOrgName;

                ctx.ResearcherExpertises.Add(re);
                ctx.SaveChanges();
                return re.ResearchExpertiseId;
            }
        }

        [HttpGet]
        public bool UpdateResearcherExpertise(int researcherExpertiseId, int expertiseId, string affilicatedOrgName)
        {
            using (ScheduleExEntities ctx = new ScheduleExEntities())
            {
                DataAccess.ResearcherExpertis re = ctx.ResearcherExpertises.FirstOrDefault(r => r.ResearchExpertiseId == researcherExpertiseId);
                if(re == null)
                {
                    return false;
                }
               else
                {
                    re.ExpertiseId = expertiseId;
                    re.AffiliatedOrgName = affilicatedOrgName;
                    return true;
                }
            }
        }

        [HttpGet]
        public bool DeleteResearcherExpertise(int researcherExpertiseId)
        {
            using (ScheduleExEntities ctx = new ScheduleExEntities())
            {
                DataAccess.ResearcherExpertis re = ctx.ResearcherExpertises.FirstOrDefault(r => r.ResearchExpertiseId == researcherExpertiseId);
                if (re == null)
                {
                    return false;
                }
                else
                {
                    ctx.ResearcherExpertises.Remove(re);
                    ctx.SaveChanges();
                    return true;
                }
            }
        }

        [HttpGet]
        public List<ResearcherExpertiseDto> GetResearcherExpertise(int researcherId)
        {
            using (ScheduleExEntities ctx = new ScheduleExEntities())
            {
                var query = from re in ctx.ResearcherExpertises where re.ResearcherId == researcherId select new ResearcherExpertiseDto() { ResearchExpertiseId = re.ResearchExpertiseId, ResearcherId = re.ResearcherId, ExpertiseId = re.ExpertiseId, Expertise = new ExpertiseDto() { ExpertiseId = re.Expertise.ExpertiseId, ExpertiseName= re.Expertise.ExpertiseName}, AffiliatedOrgName = re.AffiliatedOrgName  };
                return query.ToList();
            }
        }
    }
}
