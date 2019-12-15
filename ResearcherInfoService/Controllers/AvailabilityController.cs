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
    //public class AvailabilityController : ApiController
    //{
        //[HttpGet]
        //public List<ResearcherAvailabilityDto> GetResearcherAvailability(int researcherId)
        //{
        //    using (ScheduleExEntities ctx = new ScheduleExEntities())
        //    {
        //        var query = from a in ctx.ResearcherAvailabilities select new ResearcherAvailabilityDto() { AvailabilityId = a.AvailabilityId, ResearcherId = a.ResearcherId, StartDate = a.StartDate, EndDate = a.EndDate };
        //        return query.ToList();
        //    }
        //}

        //public UserDto GetResearcherProfile()
        //{

        //}

        //[HttpGet]
        //public int AddResearcherAvailability(int researcherId, DateTime startDate, DateTime endDate)
        //{
        //    using (ScheduleExEntities ctx = new ScheduleExEntities())
        //    {
        //        DataAccess.ResearcherAvailability ra = new DataAccess.ResearcherAvailability();
        //        ra.ResearcherId = researcherId;
        //        ra.StartDate = startDate;
        //        ra.EndDate = endDate;

        //        ctx.ResearcherAvailabilities.Add(ra);
        //        ctx.SaveChanges();
        //        return ra.AvailabilityId;
        //    }
        //}

        //[HttpGet]
        //public bool UpdateResearcherAvailability(int availabilityId, DateTime startDate, DateTime endDate)
        //{
        //    using (ScheduleExEntities ctx = new ScheduleExEntities())
        //    {
        //        DataAccess.ResearcherAvailability ra = ctx.ResearcherAvailabilities.FirstOrDefault(r => r.AvailabilityId == availabilityId);
        //        if(ra == null)
        //        {
        //            return false;
        //        }
        //       else
        //        {
        //            ra.StartDate = startDate;
        //            ra.EndDate = endDate;
        //            return true;
        //        }
        //    }
        //}

        //[HttpGet]
        //public bool DeleteResearcherAvailability(int availabilityId)
        //{
        //    using (ScheduleExEntities ctx = new ScheduleExEntities())
        //    {
        //        DataAccess.ResearcherAvailability ra = ctx.ResearcherAvailabilities.FirstOrDefault(r => r.AvailabilityId == availabilityId);
        //        if (ra == null)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            if (ra.ResearcherApprovals != null || ra.ResearcherApprovals.Count == 0)
        //            {
        //                return false;
        //            }
        //            else
        //            {
        //                ctx.ResearcherAvailabilities.Remove(ra);
        //                ctx.SaveChanges();
        //                return true;
        //            }
        //        }
        //    }
        //}
   // }
}
