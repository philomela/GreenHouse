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
        private readonly IMongoCollection<LoadedProgramBase> collection;
        private readonly string _connectionString;
        private readonly MongoUrlBuilder _connection;

        public MongoLoadedProgramRepository(string connectionString)
        {
            _connection = new MongoUrlBuilder(connectionString);
            client = new MongoClient(connectionString);
            database = client.GetDatabase(_connection.DatabaseName);
            collection = database.GetCollection<LoadedProgramBase>("LoadedProgram");
        }

        public async Task<List<LoadedProgramBase>> GetLoadedProgramListAsync()
        {
            try {
                var filter = new BsonDocument();
                var loadedPrograms = await collection.Find(filter).ToListAsync();
                return loadedPrograms;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public void Save() { }

        public void Dispose() { }

        public LoadedProgramBase GetLoadedProgram(int id)
        {
            throw new NotImplementedException();
        }

        public void Create(LoadedProgramBase item)
        {
            collection.InsertOneAsync(item);
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

