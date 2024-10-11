using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedisrceenCMS.Models.MongoDb.Models
{
    public class DoctorNotesAssessment
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public string AssessmentResult { get; set; }
    }
}
