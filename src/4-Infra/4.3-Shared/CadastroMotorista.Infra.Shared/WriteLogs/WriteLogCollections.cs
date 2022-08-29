using CadastroMotorista.Infra.Shared.Utils;
using CadastroMotorista.Infra.Shared.WriteLogs.Interface;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;

namespace CadastroMotorista.Infra.Shared.WriteLogs
{
    public class WriteLogCollections : IWriteLogCollections
    {
        private readonly MongoClient _dbClient = null;

        public WriteLogCollections()
        {
            //_dbClient = new MongoClient(Connections.GetConnectionStringMongoDB());
        }

        public void WriteError(string json)
        {
            try
            {
                var dbList = _dbClient.GetDatabase(Constants.NomeBaseMongoDB());
                var collection = dbList.GetCollection<BsonDocument>("LogErrorAPI");

                BsonDocument document = BsonSerializer.Deserialize<BsonDocument>(json);
                collection.InsertOne(document);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public void WriteRequest(string json)
        {
            try
            {
                var dbList = _dbClient.GetDatabase(Constants.NomeBaseMongoDB());
                var collection = dbList.GetCollection<BsonDocument>("Request");

                BsonDocument document = BsonSerializer.Deserialize<BsonDocument>(json);
                collection.InsertOne(document);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void WriteResponse(string json)
        {
            try
            {
                var dbList = _dbClient.GetDatabase(Constants.NomeBaseMongoDB());
                var collection = dbList.GetCollection<BsonDocument>("Response");

                BsonDocument document = BsonSerializer.Deserialize<BsonDocument>(json);
                collection.InsertOne(document);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}