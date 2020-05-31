using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class ConversationRoom
    {
        [Key]
        public string RoomName { get; set; }
        //public virtual ICollection<User> Users { get; set; }
    }
}
