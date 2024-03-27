using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Answer_Validator.DB_Classes
{
    public class Result
    {
        public int ResultId { get; set; }
        public int ResultValue { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public int UserId { get; set; }    
        public AppUser AppUser { get; set; }
    }
}
