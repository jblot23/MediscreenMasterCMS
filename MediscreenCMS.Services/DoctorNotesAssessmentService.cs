using MediscreenCMS.Models;
using MedisrceenCMS.Models;
using MedisrceenCMS.Models.MongoDb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediscreen.Services
{ 
    public class DoctorNotesAssessmentService
    {
        private readonly IMongoCollection<DoctorNotesAssessment> _doctorNotesAssessmentCollection;

        public DoctorNotesAssessmentService(
            IOptions<DoctorNoteAssessmentConnectionString> doctorNoteStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                doctorNoteStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                doctorNoteStoreDatabaseSettings.Value.DatabaseName);

            _doctorNotesAssessmentCollection = mongoDatabase.GetCollection<DoctorNotesAssessment>(
                doctorNoteStoreDatabaseSettings.Value.DoctoreAssessmentCollectionName);
        }

        public async Task<List<DoctorNotesAssessment>> GetAsync() =>
            await _doctorNotesAssessmentCollection.Find(_ => true).ToListAsync();

        public async Task<DoctorNotesAssessment?> GetAsync(string id) =>
            await _doctorNotesAssessmentCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(DoctorNotesAssessment newDoctorNote) =>
            await _doctorNotesAssessmentCollection.InsertOneAsync(newDoctorNote);

        public async Task UpdateAsync(string id, DoctorNotesAssessment updatedDoctorNote) =>
            await _doctorNotesAssessmentCollection.ReplaceOneAsync(x => x.Id == id, updatedDoctorNote);

        public async Task RemoveAsync(string id) =>
            await _doctorNotesAssessmentCollection.DeleteOneAsync(x => x.Id == id);
    }
}
