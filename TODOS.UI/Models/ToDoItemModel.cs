using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TODOS.UI.Models
{
    public class ToDoItemModel
    {
        public Guid id { get; set; }

        [Required(ErrorMessage = "The Title is mandatory")]
        [DataType(DataType.Text)]
        public string title { get; set; }

        [Required(ErrorMessage = "The Content is mandatory")]
        [DataType(DataType.Text)]
        public string content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime lastModified { get; set; }
    }
}
