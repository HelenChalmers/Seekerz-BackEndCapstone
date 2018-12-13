using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Seekerz.Models;

namespace Seekerz.Models
{
    public class QA
    {
        [Key]
        public int QAId { get; set; }



        [Required]
        public string Question { get; set; }

        public string Answer { get; set; }

        public string Notes { get; set; }

        // Set up PK -> FK relationships to other objects

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
