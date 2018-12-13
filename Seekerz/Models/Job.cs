using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Seekerz.Models;

namespace Seekerz.Models
{
    public class Job
    {
        [Key]
        public int JobId { get; set; }

        [Required]
        public string UserJob { get; set; }

        public string PersonalNotes { get; set; }

        public string ToldNss { get; set; }

        [Required]

        public bool IsActive { get; set; }




        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public virtual ICollection<QA> UserQuestionsAsked { get; set; }

        public virtual ICollection<Task> UserTasks { get; set; }

    }
}