using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class UniqueUserKey
    {
        public int Id { get; set; }
        public int UserTo { get; set; }
        public int UserFrom { get; set; }
        public int UniqueUserKeys { get; set; }
    }
}
