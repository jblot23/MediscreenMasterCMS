using MediscreenCMS.Models;
using MedisrceenCMS.Models.MongoDb.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediscreenCMS.Services
{
    public class DoctorNotesServices
    {
        private readonly IMongoCollection<DoctorNotes> _doctorNotesCollection;

        public DoctorNotesServices(
            IOptions<DoctorNoteConnectionString> doctorNoteStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                doctorNoteStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                doctorNoteStoreDatabaseSettings.Value.DatabaseName);

            _doctorNotesCollection = mongoDatabase.GetCollection<DoctorNotes>(
                doctorNoteStoreDatabaseSettings.Value.DoctorNotesCollectionName);
        }

        public async Task<List<DoctorNotes>> GetAsync() =>
            await _doctorNotesCollection.Find(_ => true).ToListAsync();

        public async Task<DoctorNotes?> GetAsync(string id) =>
            await _doctorNotesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(DoctorNotes newDoctorNote) =>
            await _doctorNotesCollection.InsertOneAsync(newDoctorNote);

        public async Task UpdateAsync(string id, DoctorNotes updatedDoctorNote) =>
            await _doctorNotesCollection.ReplaceOneAsync(x => x.Id == id, updatedDoctorNote);

        public async Task RemoveAsync(string id) =>
            await _doctorNotesCollection.DeleteOneAsync(x => x.Id == id);
    }
}