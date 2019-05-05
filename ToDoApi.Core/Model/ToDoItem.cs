using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ToDoApi.Core.Model
{
    public class ToDoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ItemId { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }
        public DateTimeOffset? CompletedOn { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public long ToDoListId { get; set; }

        public virtual ToDoList ToDoList { get; set; }
    }
}
