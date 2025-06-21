using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity.Sys
{
    public class RegistrationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public User User { get; set; }
    }
}
