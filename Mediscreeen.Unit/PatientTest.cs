using Mediscreen.Services;
using MediscreenCMS.Data;
using MediscreenCMS.Models;
using MediscreenCMS.Services;
using MedisrceenCMS.Models; 
using MedisrceenCMS.Models.MongoDb.Models;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;

namespace Mediscreeen.Unit
{
    public class Tests
    {
        private Mock<IMongoCollection<Patient>> _mockPatientCollection;
        private PatientService _patientService;
        private DoctorNotesServices _doctorNotesServices;
        private DoctorNotesAssessmentService _doctorNotesAssessmentService;  

        [SetUp]
        public void Setup()
        {
            // Mock the IMongoCollection<Patient>
            _mockPatientCollection = new Mock<IMongoCollection<Patient>>();

            // Mock the IOptions<PatientConnectionString>
            var mockOptions = new Mock<IOptions<PatientConnectionString>>();
            mockOptions.Setup(x => x.Value).Returns(new PatientConnectionString
            {
                ConnectionString = "mongodb://localhost:27017/",
                DatabaseName = "MediScreen",
                PatientCollectionName = "Patients"
            });

            var mockOptionsDoc = new Mock<IOptions<DoctorNoteConnectionString>>();
            mockOptionsDoc.Setup(x => x.Value).Returns(new DoctorNoteConnectionString
            {
                ConnectionString = "mongodb://localhost:27017/",
                DatabaseName = "MediScreen",
                DoctorNotesCollectionName = "DoctorNotes"
            });

            var mockOptionsDocAssessment = new Mock<IOptions<DoctorNoteAssessmentConnectionString>>();
            mockOptionsDocAssessment.Setup(x => x.Value).Returns(new DoctorNoteAssessmentConnectionString
            {
                ConnectionString = "mongodb://localhost:27017/",
                DatabaseName = "MediScreen",
                DoctoreAssessmentCollectionName = "DoctorNotesAssessment"
            });

            // Create the service with the mocked IMongoCollection
            _patientService = new PatientService(mockOptions.Object);
            _doctorNotesServices = new DoctorNotesServices(mockOptionsDoc.Object);
            _doctorNotesAssessmentService = new DoctorNotesAssessmentService(mockOptionsDocAssessment.Object);

        
        }

        [Test,Order(1)]
        public void CreatePatient()
        {

            List<Patient> patients = new List<Patient>
            {
                new Patient { Id = "1", FirstName = "Ferguson", DateOFBirth = new DateTime(1968, 6, 22), Gender = "M", Address = "2 Warren Street", PhoneNumber = "387-866-1399" },
                new Patient { Id = "2", FirstName = "Rees", DateOFBirth = new DateTime(1952, 9, 27), Gender = "F", Address = "745 West Valley Farms Drive", PhoneNumber = "628-423-0993" },
                new Patient { Id = "3", FirstName = "Arnold", DateOFBirth = new DateTime(1952, 11, 11), Gender = "M", Address = "599 East Garden Ave", PhoneNumber = "123-727-2779" },
                new Patient { Id = "4", FirstName = "Sharp", DateOFBirth = new DateTime(1946, 11, 26), Gender = "M", Address = "894 Hall Street", PhoneNumber = "451-761-8383" },
                new Patient { Id = "5", FirstName = "Ince", DateOFBirth = new DateTime(1958, 6, 29), Gender = "F", Address = "4 Southampton Road", PhoneNumber = "802-911-9975" },
                new Patient { Id = "6", FirstName = "Ross", DateOFBirth = new DateTime(1949, 12, 7), Gender = "F", Address = "40 Sulphur Springs Dr", PhoneNumber = "131-396-5049" },
                new Patient { Id = "7", FirstName = "Wilson", DateOFBirth = new DateTime(1966, 12, 31), Gender = "F", Address = "12 Cobblestone St", PhoneNumber = "300-452-1091" },
                new Patient { Id = "8", FirstName = "Buckland", DateOFBirth = new DateTime(1945, 6, 24), Gender = "M", Address = "193 Vale St", PhoneNumber = "833-534-0864" },
                new Patient { Id = "9", FirstName = "Clark", DateOFBirth = new DateTime(1964, 6, 18), Gender = "F", Address = "12 Beechwood Road", PhoneNumber = "241-467-9197" },
                new Patient { Id = "10", FirstName = "Bailey ", DateOFBirth = new DateTime(1959, 6, 28), Gender = "M", Address = "1202 Bumble Dr", PhoneNumber = "747-815-0557" }
            };

            foreach(var patient in patients)
            {
                _patientService.CreateAsync(patient).Wait();
            }

             
        }


        [Test,Order(2)]
        public void GeenrateDoctorNotes()
        { 

            List<DoctorNotes> notes = new List<DoctorNotes>
            {
                new DoctorNotes() { Id="1", PatientId = "1", DoctorNote = "Patient states that they are \"feeling terrific\"\r\nWeight at or below recommended level" },
                new DoctorNotes() { Id="2", PatientId = "1", DoctorNote = "Patient states that they feel tired during the day\r\nPatient also complains about muscle aches\r\nLab reports Microalbumin elevated" },
                new DoctorNotes() { Id="3", PatientId = "1", DoctorNote = "Patient states that they not feeling as tired\r\nSmoker, quit within last year\r\nLab results indicate Antibodies present elevated" },
                new DoctorNotes() { Id="4", PatientId = "2", DoctorNote = "Patient states that they are feeling a great deal of stress at work\r\nPatient also complains that their hearing seems Abnormal as of late" },
                new DoctorNotes() { Id="5", PatientId = "2", DoctorNote = "Patient states that they have had a Reaction to medication within last 3 months\r\nPatient also complains that their hearing continues to be Abnormal" },
                new DoctorNotes() { Id="6", PatientId = "2", DoctorNote = "Lab reports Microalbumin elevated" },
                new DoctorNotes() { Id="7", PatientId = "2", DoctorNote = "Patient states that everything seems fine\r\nLab reports Hemoglobin A1C above recommended level\r\nPatient admits to being long term Smoker" },
                new DoctorNotes() { Id="8", PatientId = "3", DoctorNote = "Patient states that they are short term Smoker" },
                new DoctorNotes() { Id="9", PatientId = "3", DoctorNote = "Lab reports Microalbumin elevated" },
                new DoctorNotes() { Id="10", PatientId = "3", DoctorNote = "Patient states that they are a Smoker, quit within last year\r\nPatient also complains that of Abnormal breathing spells\r\nLab reports Cholesterol LDL high" },
                new DoctorNotes() { Id="11", PatientId = "3", DoctorNote = "Lab reports Cholesterol LDL high" },
                new DoctorNotes() { Id="12", PatientId = "4", DoctorNote = "Patient states that walking up stairs has become difficult\r\nPatient also complains that they are having shortness of breath\r\nLab results indicate Antibodies present elevated\r\nReaction to medication" },
                new DoctorNotes() { Id="13", PatientId = "4", DoctorNote = "Patient states that they are experiencing back pain when seated for a long time" },
                new DoctorNotes() { Id="14", PatientId = "4", DoctorNote = "Patient states that they are a short term Smoker\r\nHemoglobin A1C above recommended level" },
                new DoctorNotes() { Id="15", PatientId = "5", DoctorNote = "Patient states that they experiencing occasional neck pain\r\nPatient also complains that certain foods now taste different\r\nApparent Reaction to medication\r\nBody Weight above recommended level" },
                new DoctorNotes() { Id="16", PatientId = "5", DoctorNote = "Patient states that they had multiple dizziness episodes since last visit\r\nBody Height within concerned level" },
                new DoctorNotes() { Id="17", PatientId = "5", DoctorNote = "Patient states that they are still experiencing occasional neck pain\r\nLab reports Microalbumin elevated\r\nSmoker, quit within last year" },
                new DoctorNotes() { Id="18", PatientId = "5", DoctorNote = "Patient states that they had multiple dizziness episodes since last visit\r\nLab results indicate Antibodies present elevated" },
    new DoctorNotes() { Id="19", PatientId = "6", DoctorNote = "Patient states that they feel fine\r\nBody Weight above recommended level" },
    new DoctorNotes() { Id="20", PatientId = "6", DoctorNote = "Patient states that they feel fine" },
    new DoctorNotes() { Id="21", PatientId = "7", DoctorNote = "Patient states that they often wake with stiffness in joints\r\nPatient also complains that they are having difficulty sleeping\r\nBody Weight above recommended level\r\nLab reports Cholesterol LDL high" },
    new DoctorNotes() { Id="22", PatientId = "8", DoctorNote = "Lab results indicate Antibodies present elevated\r\nHemoglobin A1C above recommended level" },
    new DoctorNotes() { Id="23", PatientId = "9", DoctorNote = "Patient states that they are having trouble concentrating on school assignments\r\nHemoglobin A1C above recommended level" },
    new DoctorNotes() { Id="24", PatientId = "9", DoctorNote = "Patient states that they are frustrated as long wait times\r\nPatient also complains that food in the vending machine is sub-par\r\nLab reports Abnormal blood cell levels" },
    new DoctorNotes() { Id="25", PatientId = "9", DoctorNote = "Patient states that they are easily irritated at minor things\r\nPatient also complains that neighbors vacuuming is too loud\r\nLab results indicate Antibodies present elevated" },
    new DoctorNotes() { Id="26", PatientId = "10", DoctorNote = "Patient states that they are not experiencing any problems" },
    new DoctorNotes() { Id="27", PatientId = "10", DoctorNote = "Patient states that they are not experiencing any problems\r\nBody Height within concerned level\r\nHemoglobin A1C above recommended level" },
    new DoctorNotes() { Id="28", PatientId = "10", DoctorNote = "Patient states that they are not experiencing any problems\r\nBody Weight above recommended level\r\nPatient reports multiple dizziness episodes since last visit" },
    new DoctorNotes() { Id="29", PatientId = "10", DoctorNote = "Patient states that they are not experiencing any problems\r\nLab reports Microalbumin elevated" }
            }; 

            foreach (var note in notes)
            {
                _doctorNotesServices.CreateAsync(note).Wait();
            }

        }

        [Test,Order(3)]
        public void CompletePatientDiabieticAssessment()
        {
            var result = _patientService.GetAsync().Result;
            foreach(var paitent in result)
            {
                var response =  DoctorAssessmentGenerator(paitent.Id).Result;
                _doctorNotesAssessmentService.CreateAsync(new DoctorNotesAssessment()
                {
                    Id = Guid.NewGuid().ToString(),
                    AssessmentResult = response ,
                    PatientId = paitent.Id,
                }).Wait();
            } 

        }

        [Test,Order(4)]
        public void MigrateDataFromMongoDbToSql()
        {

            var options = new DbContextOptionsBuilder<MedisrceenCMS.Models.Models.MediScreenContext>()
                 .UseSqlServer("Data Source=localhost;Initial Catalog=MediScreen;Integrated Security=True;Trust Server Certificate=True;")
                 .Options;

            var dbContext = new MedisrceenCMS.Models.Models.MediScreenContext(options);

            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureCreated();


            var result = _patientService.GetAsync().Result;
            foreach (var paitent in result)
            {
                dbContext.Patients.Add(new MedisrceenCMS.Models.Models.Patient()
                {
                    FirstName = paitent.FirstName,  
                    LastName =  "",
                    Address = paitent.Address,
                    City = "",
                    Phone = paitent.PhoneNumber,
                    Email = ""
                });

                dbContext.SaveChanges();
            }

        }


        public async Task<string> DoctorAssessmentGenerator(string patientId)
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