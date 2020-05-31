using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class Message
    {
        public Message()
        {

        }
        public int Id { get; set; }
        public string MessageDescription { get; set; }
        [ForeignKey("Reciever")]
        public int? UserTo { get; set; }
        [ForeignKey("Sender")]
        public int? UserFrom { get; set; }
        public virtual User Reciever { get; set; }
        public virtual User Sender { get; set; }
        public DateTime MessageDate { get; set; }
        public int RelationKey { get; set; }


    }
}
