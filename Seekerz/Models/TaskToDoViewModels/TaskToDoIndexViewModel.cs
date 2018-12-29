using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seekerz.Models.TaskToDoViewModels
{
    public class TaskToDoIndexViewModel
    {
        public Job job { get; set; }

        public List<TaskToDo> tasktodo { get; set; }
    }
}
