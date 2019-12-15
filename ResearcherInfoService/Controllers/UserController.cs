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
    public class UserController : ApiController
    {
        [HttpGet]
        public bool EmailExists(string email)
        {
            //checks email
            using (DataAccess.ScheduleExEntities ctx = new DataAccess.ScheduleExEntities())
            {
                return ctx.Users.Any(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));
            }
        }

        [HttpGet]
        public int RegisterUser(string firstName, string lastName, string email, string hashPwd, string city, string state, long phonenumber, int[] expertiseIds, string organization, string almaMater, int[] availableMonths)
        {
            using (DataAccess.ScheduleExEntities ctx = new DataAccess.ScheduleExEntities())
            {
                DataAccess.User newUser = new DataAccess.User();
                newUser.FirstName = firstName;
                newUser.LastName = lastName;
                newUser.Email = email;
                newUser.HashPwd = hashPwd;
                newUser.City = city;
                newUser.State = state;
                newUser.RoleId = ctx.Roles.First(r => r.RoleName.Equals("Researcher", StringComparison.InvariantCultureIgnoreCase)).RoleId;
                newUser.PhoneNumber = phonenumber;
                newUser.Organization = organization;
                newUser.AlmaMater = almaMater;
                ctx.Users.Add(newUser);
                ctx.SaveChanges();
                foreach(int expertiseId in expertiseIds)
                {
                    DataAccess.ResearcherExpertis expertise = new DataAccess.ResearcherExpertis();
                    expertise.ExpertiseId = expertiseId;
                    expertise.ResearcherId = newUser.UserId;
                    ctx.ResearcherExpertises.Add(expertise);
                }

                foreach (int availableMonth in availableMonths)
                {
                    DataAccess.ResearcherAvailability availability = new DataAccess.ResearcherAvailability();
                    availability.Month = availableMonth;
                    availability.ResearcherId = newUser.UserId;
                    ctx.ResearcherAvailabilities.Add(availability);
                }
                ctx.SaveChanges();

                return newUser.UserId;
            }

        
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
    }
}