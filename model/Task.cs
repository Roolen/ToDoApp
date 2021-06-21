using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.model
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }
        public string Text { get; set; }
        public bool isComplete { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
