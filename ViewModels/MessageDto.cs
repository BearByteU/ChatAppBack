using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.ViewModels
{
    public class MessageDto
    {
        public string MessageDescription { get; set; }
        [Required]
        public int UserTo { get; set; }
        public int UserFrom { get; set; }
        public DateTime MessageDate { get; set; }
    }
}
