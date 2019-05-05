using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ToDoApi.Core.Model
{
    public class ToDoList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ListId { get; set; }
        public string UserId { get; set; }
        public string ListName { get; set; }

        public virtual IEnumerable<ToDoItem> ToDoItems { get; set; }
    }
}
