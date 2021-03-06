using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationUser : IdentityUser
    {

        public virtual Cart Cart { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
