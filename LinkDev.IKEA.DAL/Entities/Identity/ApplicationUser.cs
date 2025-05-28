using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.IKEA.DAL.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public  required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
