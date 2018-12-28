using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seekerz.Models
{
    public class TaskToDo
    {
        [Key]
        public int TaskToDoId { get; set; }

        [Required]
        public string NewTask { get; set; }

        public DateTime CompleteDate { get; set; }

        public bool IsCompleted { get; set; }

        [Required]
        [Display(Name = "Position")]
        public int JobId { get; set; }

        public Job Jobs { get; set; }

        
    }
}
