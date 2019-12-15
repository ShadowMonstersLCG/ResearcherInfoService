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
        [HttpGet]
        List<ProjectDto> GetProjects(int researcherId)
        {
            List<ProjectDto> projectDtos = new List<ProjectDto>();
            using (ScheduleExEntities ctx = new ScheduleExEntities())
            {
                DataAccess.User user = ctx.Users.First(u => u.UserId == researcherId);

                List<ResearcherAvailability> availabilities = user.ResearcherAvailabilities.ToList();

                //get all projects
                List<Project> projects = ctx.Projects.Where(p => p.IsPublished == true && p.Approved == true).ToList();
               
                for(int projIndex = 0; projIndex < projects.Count; projIndex++)
                {
                    List<ResearcherApproval> approvals = ctx.ResearcherApprovals.Where(a => a.ResearcherId == researcherId && a.ProjectId == projects[projIndex].ProjectId).ToList();

                    if (projects[projIndex].State.Equals(user.State))
                    {
                        continue;
                    }

                    bool hasAvailabilityMatch = false;
                    DataAccess.ResearcherApproval researcherApproval = null;
                    foreach(ResearcherAvailability availability in availabilities)
                    {
                        if(availability.Month >= projects[projIndex].StartDate.Month && availability.Month <= projects[projIndex].EndDate.Month)
                        {
                            hasAvailabilityMatch = true;

                            researcherApproval = user.ResearcherApprovals.Where(ra => ra.ProjectId == projects[projIndex].ProjectId).FirstOrDefault();
                        }
                    }

                    if(!hasAvailabilityMatch)
                    {
                        continue;
                    }

                    ProjectDto projectDto = new ProjectDto();
                    projectDto.ProjectId = projects[projIndex].ProjectId;
                    projectDto.ProjectName = projects[projIndex].ProjectName;
                    projectDto.Description = projects[projIndex].Description;
                    projectDto.State = projects[projIndex].State;
                    projectDto.StartDate = projects[projIndex].StartDate;
                    projectDto.EndDate = projects[projIndex].EndDate;
                    projectDto.Status = (researcherApproval != null ? researcherApproval.ApprovalStatus.Status : "Available");
                    projectDto.InfoRequested = (researcherApproval != null ? researcherApproval.InfoRequested : string.Empty);
                    projectDtos.Add(projectDto);
                }
            }
            return projectDtos;
        }

        [HttpGet]
        public bool ApplyForProject(int researcherId, int projectId)
        {
            using (ScheduleExEntities ctx = new ScheduleExEntities())
            {
                Project project = ctx.Projects.FirstOrDefault(p => p.ProjectId == projectId);
                User user = ctx.Users.FirstOrDefault(u => u.UserId == researcherId);

                int noOfMatches = project.Expertises.Select(ex => ex.ExpertiseId).ToList().Intersect(user.ResearcherExpertises.Select(ex => ex.ExpertiseId).ToList()).Count();

                string matchScore = string.Format("{0}/{1}", noOfMatches, project.Expertises.Count);

                if (ctx.ResearcherApprovals.Any(appr => appr.ResearcherId == researcherId && appr.ProjectId == projectId))
                {
                    return false;
                }
                else
                {
                    ResearcherApproval ra = new ResearcherApproval();
                    ra.ApprovalStatusId = Constants.APPROVAL_STS_APPLIED;
                    ra.ProjectId = projectId;
                    ra.ResearcherId = researcherId;
                    ra.HasResearcherApplied = true;
                    ra.ExpertiseMatchScore = matchScore;
                    ctx.ResearcherApprovals.Add(ra);
                    ctx.SaveChanges();
                    return true;
                }
            }
        }

        [HttpGet]
        public List<ResearcherApprovalDto> GetResearcherApprovalStatus(int researcherId)
        {
            using (ScheduleExEntities ctx = new ScheduleExEntities())
            {
                var query = from ra in ctx.ResearcherApprovals where ra.ResearcherId == researcherId select new ResearcherApprovalDto { ResearcherId = ra.ResearcherId, ApprovalStatus = ra.ApprovalStatus.Status, ProjectName = ra.Project.ProjectName, InfoRequested = ra.InfoRequested };
                return query.ToList();
            }
        }

        [HttpGet]
        public bool SaveInformationRequested(int researcher, int projectId, string informationRequested)
        {
            using (ScheduleExEntities ctx = new ScheduleExEntities())
            {
                ResearcherApproval approval = ctx.ResearcherApprovals.FirstOrDefault(ra => ra.ResearcherId == researcher && ra.ProjectId == projectId);
                approval.InfoRequested = informationRequested;
                approval.ApprovalStatusId = Constants.APPROVAL_STS_APPLIED;
                ctx.SaveChanges();
                return true;
            }
        }
     }
}