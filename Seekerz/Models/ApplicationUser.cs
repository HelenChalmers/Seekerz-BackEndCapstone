using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Seekerz.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        // Set up PK -> FK relationships to other objects
        public virtual ICollection<Job> Jobs { get; set; }

        public virtual ICollection<QA> QAs { get; set; }

        public virtual ICollection<Company> Companies { get; set; }

        public virtual ICollection<TaskToDo> TasksToDo { get; set; }
    }
}
