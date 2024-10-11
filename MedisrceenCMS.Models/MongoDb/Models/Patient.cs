using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedisrceenCMS.Models.MongoDb.Models
{
    public class Patient
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOFBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
