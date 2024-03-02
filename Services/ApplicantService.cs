using mortgage_application.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace mortgage_application.Services
{
    public class ApplicantService
    { 
    
        private readonly IMongoCollection<Applicant> _applicantCollection;

        public ApplicantService(
            IOptions<MortgageDatabaseSettings> mortgageDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                mortgageDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mortgageDatabaseSettings.Value.DatabaseName);

            _applicantCollection = mongoDatabase.GetCollection<Applicant>(
                mortgageDatabaseSettings.Value.MortgageCollectionName);
        }

        public async Task<List<Applicant>> GetAsync() =>
            await _applicantCollection.Find(_ => true).ToListAsync();

        public async Task<Applicant?> GetAsync(string id) =>
            await _applicantCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Applicant newApplicant)
        {
            await _applicantCollection.InsertOneAsync(newApplicant);
        }

        public async Task UpdateAsync(string id, Applicant updatedApplicant) =>
            await _applicantCollection.ReplaceOneAsync(x => x.Id == id, updatedApplicant);

        public async Task RemoveAsync(string id) =>
            await _applicantCollection.DeleteOneAsync(x => x.Id == id);
        }
}
