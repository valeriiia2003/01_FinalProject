using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Answer_Validator.DB_Classes
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public ICollection<Result> Results { get; set; }
    }
}
