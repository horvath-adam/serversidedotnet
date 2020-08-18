using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventApp.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public bool Enabled { get; set; }

        public DateTime DoB { get; set; }

        public virtual ICollection<IdentityUserRole<int>> Roles { get; } = new List<IdentityUserRole<int>>();

    }
}
