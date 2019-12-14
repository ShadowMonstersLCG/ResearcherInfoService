using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResearcherInfoService.Models
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string HashPwd { get; set; }
        public int RoleId { get; set; }
        public long PhoneNumber { get; set; }
    }
}