using System;
using greenhouse_app.Data.Models;
using greenhouse_app.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Configuration;

namespace greenhouse_app.Implementations
{
    public class MongoLoadedProgramRepository : IRepository<LoadedProgramBase>
    {
        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<BsonDocument> collection;
        private readonly string _connectionString;

        public MongoLoadedProgramRepository(string connectionString)
        {
            var connection = new MongoUrlBuilder(connectionString);

            client = new MongoClient(connectionString);
            database = client.GetDatabase(connection.DatabaseName);
            collection = database.GetCollection<BsonDocument>("LoadedProgram");
        }

        public IMongoCollection<LoadedProgramBase> LoadedPrograms
        {
            get
            {
                return database.GetCollection<LoadedProgramBase>("LoadedProgram");
            }
        }

        public async Task<List<BsonDocument>> GetLoadedProgramListAsync()
        {
            
            var filter = new BsonDocument();
            var loadedPrograms = await collection.Find(filter).ToListAsync();
            return loadedPrograms;
        }

        public void Save() { }

        public void Dispose() { }

        public IEnumerable<LoadedProgramBase> GetLoadedProgramList()
        {
            throw new NotImplementedException();
        }

        public LoadedProgramBase GetLoadedProgram(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(LoadedProgramBase item)
        {
            throw new NotImplementedException();
        }

        public void Update(LoadedProgramBase item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

