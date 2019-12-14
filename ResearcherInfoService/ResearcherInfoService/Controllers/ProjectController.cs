using ResearcherInfoService.DataAccess;
using ResearcherInfoService.Helpers;
using ResearcherInfoService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ResearcherInfoService.Controllers
{
    public class ProjectController : ApiController
    {
        List<ProjectDto> GetProjectsForResearcher(int researcherId)
        {
            using (ScheduleExEntities ctx = new ScheduleExEntities())
            {
                //get all availabilities
                List<ResearcherAvailability> availabilities = ctx.ResearcherAvailabilities.Where(r => r.ResearcherId == researcherId).ToList();
                List<int> projectIdsAlreadyApplied = new List<int>();
                //filter out that are already applied 
                for (int i = 0; i < availabilities.Count; i++)
                {
                    ResearcherApproval approvalItem = ctx.ResearcherApprovals.FirstOrDefault(appr => appr.ResearcherAvailabilityId == availabilities[i].AvailabilityId);
                    if (approvalItem != null)
                    {
                        projectIdsAlreadyApplied.Add(approvalItem.ProjectId);
                    }
                }
                //get all projects
                List<Project> projects = ctx.Projects.Where(p => p.IsPublished == true && p.Approved == true).ToList();
                //filter out that doesnt fall in availability range.
                for(int j = 0; j < projects.Count; j++)
                {
                    if(projectIdsAlreadyApplied.Contains(projects[j].ProjectId) ||  !availabilities.Any(a => projects[j].StartDate <= a.StartDate && projects[j].EndDate >= a.EndDate))
                    {
                        projects.Remove(projects[j]);
                    }
                }

                List<ProjectDto> projectDto = (from p in projects select new ProjectDto { ProjectId = p.ProjectId, StartDate = p.StartDate, EndDate = p.EndDate, ProjectName = p.ProjectName, Description = p.Description,  ApprovedBy = p.ApprovedBy, ApprovedDate = p.ApprovedDate, Approved = p.Approved, IsPublished = p.IsPublished }).ToList();
                return projectDto;
            }
        }

        public bool ApplyForProject(int researcherAvailabilityId, int projectId)
        {
            using (ScheduleExEntities ctx = new ScheduleExEntities())
            {
                if(ctx.ResearcherApprovals.Any(appr => appr.ResearcherAvailabilityId == researcherAvailabilityId && appr.ProjectId == projectId))
                {
                    return false;
                }
                else
                {
                    ResearcherApproval ra = new ResearcherApproval();
                    ra.ApprovalStatusId = Constants.APPROVAL_STS_NOT_STARTED;
                    ra.ProjectId = projectId;
                    ra.ResearcherAvailabilityId = researcherAvailabilityId;
                    ra.ResearcherId = ctx.ResearcherAvailabilities.First(r => r.AvailabilityId == researcherAvailabilityId).ResearcherId;
                    ctx.ResearcherApprovals.Add(ra);
                    ctx.SaveChanges();
                    return true;
                }
            }
        }

        public List<ResearcherApprovalDto> GetResearcherApprovalStatus(int researcherId)
        {
            using (ScheduleExEntities ctx = new ScheduleExEntities())
            {
                var query = from ra in ctx.ResearcherApprovals where ra.ResearcherId == researcherId select new ResearcherApprovalDto { ResearcherId = ra.ResearcherId, ApprovalStatus = ra.ApprovalStatus.Status, ProjectName = ra.Project.ProjectName, InfoRequested = ra.InfoRequested };
                return query.ToList();
            }
        }

        public bool SaveInformationRequested(int availabilityId, int projectId, string informationRequested)
        {
            using (ScheduleExEntities ctx = new ScheduleExEntities())
            {
                ResearcherApproval approval = ctx.ResearcherApprovals.FirstOrDefault(ra => ra.ResearcherAvailabilityId == availabilityId && ra.ProjectId == projectId);
                approval.InfoRequested = informationRequested;
                ctx.SaveChanges();
                return true;
            }
        }
     }
}