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
        public string Position { get; set; }

        public string PersonalNotes { get; set; }

        public string ToldNss { get; set; }

        [Required]
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public int CompanyId { get; set; }
        [Required]
        public Company Company { get; set; }

        public virtual ICollection<TaskToDo> UserTasks { get; set; }

    }
}