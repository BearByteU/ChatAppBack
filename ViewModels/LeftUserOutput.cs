using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.ViewModels
{
    public class LeftUserOutput
    {
        public int UserId { get; set; }
        public string Message { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
