 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using MediscreenCMS.Services;

namespace MediscreenCMS.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private readonly DoctorNotesServices _doctorNotesServices;
        private readonly PatientService _patientService;

        public PatientController(ILogger<PatientController> logger, DoctorNotesServices doctorNotesServices, PatientService patientService)
        {
            _logger = logger;
            _doctorNotesServices = doctorNotesServices;
            _patientService=patientService;
        }

        [Route("patient/add")]
        public async Task<IActionResult> AddPatient(MedisrceenCMS.Models.MongoDb.Models.DoctorNotes doctorNotes)
        {
            await _doctorNotesServices.CreateAsync(doctorNotes);

            return Ok();
        }

        [Route("patHistory/add")] 
        public async Task<IActionResult> AddPatientHistory(MedisrceenCMS.Models.MongoDb.Models.Patient patient)
        {
            await _patientService.CreateAsync(patient);

            return Ok();
        }

        [Route("patientRiskLevel/get")]
        public async Task<string> GetPatientRiskLevel(string patientId)
        {
            var patient = await _patientService.GetAsync(patientId);
            var patientNotes = _doctorNotesServices.GetAsync().Result.Where(x => x.PatientId == patientId).ToList();

            var savedTriggerTerms = new List<string>() {
                "Hemoglobin A1C",
                "Microalbumin",
                "Body Height",
                "Body Weight",
                "Smoker",
                "Abnormal",
                "Cholesterol",
                "Dizziness",
                "Relapse",
                "Reaction",
                "Antibodies"
            };

            // Find patient age
            DateTime birthDate = patient.DateOFBirth;
            int age = DateTime.Today.Year - birthDate.Year - (DateTime.Today < birthDate.AddYears(DateTime.Today.Year - birthDate.Year) ? 1 : 0);

            int triggerCount = 0;

            // None: No doctor notes containing any of the trigger terms
            foreach (var note in patientNotes)
            {
                foreach (var term in savedTriggerTerms)
                {
                    if (note.DoctorNote.Contains(term, StringComparison.OrdinalIgnoreCase))
                    {
                        triggerCount++;
                        break; // Trigger term found, no need to check more terms for this note
                    }
                }
            }

            if (triggerCount == 0)
                return "Patient has no risk level";

            // Borderline: Age > 30 and 2 trigger terms
            if (age > 30 && triggerCount == 2)
            {
                return "Patient risk level is borderline";
            }

            // In Danger: Male < 30 (3 triggers), Female < 30 (4 triggers), Age > 30 (6 triggers)
            if (age < 30 && patient.Gender == "Male" && triggerCount == 3)
            {
                return "Patient risk level is in danger";
            }
            else if (age < 30 && patient.Gender == "Female" && triggerCount == 4)
            {
                return "Patient risk level is in danger";
            }
            else if (age > 30 && triggerCount == 6)
            {
                return "Patient risk level is in danger";
            }

            // Early Onset: Male < 30 (5 triggers), Female < 30 (7 triggers), Age > 30 (8 triggers)
            if (age < 30 && patient.Gender == "Male" && triggerCount == 5)
            {
                return "Patient risk level is early onset";
            }
            else if (age < 30 && patient.Gender == "Female" && triggerCount == 7)
            {
                return "Patient risk level is early onset";
            }
            else if (age > 30 && triggerCount >= 8)
            {
                return "Patient risk level is early onset";
            }

            return "Patient has no issue";
        }



    }
}
