using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MedisrceenCMS.Models.MongoDb.Models;
using MediscreenCMS.Models;

namespace MediscreenCMS.Services
{
    public class PatientService
    {
        private readonly IMongoCollection<Patient> _patientCollection;

        public PatientService(
            IOptions<PatientConnectionString> patientDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                patientDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                patientDatabaseSettings.Value.DatabaseName);

            _patientCollection = mongoDatabase.GetCollection<Patient>(
                "Patients");
        }

        public async Task<List<Patient>> GetAsync() =>
            await _patientCollection.Find(_ => true).ToListAsync();

        public async Task<Patient?> GetAsync(string id) =>
            await _patientCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Patient newPatient) =>
            await _patientCollection.InsertOneAsync(newPatient);

        public async Task UpdateAsync(string id, Patient updatedPatient) =>
            await _patientCollection.ReplaceOneAsync(x => x.Id == id, updatedPatient);

        public async Task RemoveAsync(string id) =>
            await _patientCollection.DeleteOneAsync(x => x.Id == id);
    }

}
