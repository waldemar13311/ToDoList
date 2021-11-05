using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class ToDoItem
    {
        public long Id { get; set; }
        
        [Required]
        public string Item { get; set; }
        public bool IsCompleted { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
