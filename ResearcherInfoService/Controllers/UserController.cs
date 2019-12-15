using ResearcherInfoService.DataAccess;
using ResearcherInfoService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
        public int RegisterUser(string firstName, string lastName, string email, string hashPwd, string city, string state, long phonenumber, string expertiseIdsStr, string organization, string almaMater, string availableMonthsStr)
        {
            try
            {
                using (DataAccess.ScheduleExEntities ctx = new DataAccess.ScheduleExEntities())
                {
                    if(ctx.Users.Any(u => u.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        throw new Exception(string.Format("Email '{0}' already exists.", email));
                    }

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
                    List<int> expertiseIds = expertiseIdsStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ConvertAll(int.Parse);
                    foreach (int expertiseId in expertiseIds)
                    {
                        DataAccess.ResearcherExpertis expertise = new DataAccess.ResearcherExpertis();
                        expertise.ExpertiseId = expertiseId;
                        expertise.ResearcherId = newUser.UserId;
                        ctx.ResearcherExpertises.Add(expertise);
                    }
                    List<int> availableMonths = availableMonthsStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList().ConvertAll(int.Parse);
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
            catch (DbEntityValidationException e)
            {
                string error = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);

                        error += ve.ErrorMessage;
                    }

                   
                }
                throw new Exception("Validation Failed." + error);
            }
            catch(System.Data.Entity.Infrastructure.DbUpdateException ue)
            {
                throw;
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