using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace api.fdevs.com.Models
{
    public class ToDoItemViewModel
    {
        public int ToDoItemId { get; set; }
        [Required]
        public Guid APIKey { get; set; }
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [MaxLength(50, ErrorMessage = "ToDoItem cannot be longer than 50 characters.")]
        [MinLength(1, ErrorMessage = "ToDoItem cannot be empty.")]
        public string ToDoItem { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsArchived { get; set; }
    }
}