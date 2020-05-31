﻿using ChatApp.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string username { get; set; }
        //public ICollection<Connection> Connections { get; set; }
        //public virtual ICollection<ConversationRoom> Rooms { get; set; }
        public DateTime CreationDate { get; set; }
        //public Gender Gender { get; set; }
    }
}
