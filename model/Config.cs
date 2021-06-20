using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.model
{
    class Config
    {
        [Key]
        public int ConfigId { get; set; }
        public int UserAuthId { get; set; }
    }
}
