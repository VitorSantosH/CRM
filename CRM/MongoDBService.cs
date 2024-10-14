using MongoDB.Driver;
using System.Text.Json;
using MongoDB.Bson;
using System.Collections.Generic;
using CRM.Entidades;


namespace CRM
{
    public class MongoDBService
    {
        private readonly IMongoDatabase _database;

        private static string connectionString { get; set; } = "mongodb://localhost:27017/";

        public MongoDBService()
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("CRM"); // Nome do banco de dados
        }

        public async Task<Usuario?> InsertUser(Usuario user)
        {
            var collection = _database.GetCollection<Usuario>("users"); // Nome da coleção

            try
            {
                await collection.InsertOneAsync(user);

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;   
            }
         
        }

        public void DeleteUser(ObjectId id)
        {
            var collection = _database.GetCollection<BsonDocument>("users");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            collection.DeleteOne(filter);
        }

        public async Task<IAsyncCursor<BsonDocument>> GetUser(ObjectId id)
        {
            var collection = _database.GetCollection<BsonDocument>("users");
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var Ret = await collection.FindAsync(filter);

            return Ret;
        }

        public async Task<Usuario?> GetUser(string  email)
        {
            var collection = _database.GetCollection<BsonDocument>("users");
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("email", email);
            var userDocument = await collection.Find(filter).FirstOrDefaultAsync();

            if (userDocument == null)
            {
                return null; // Ou lance uma exceção, conforme sua lógica
            }

            // Converta o documento BsonDocument em um objeto User
            return new Usuario
            {
                Id = userDocument["_id"].AsObjectId,
                Email = userDocument["email"].ToString(),
                Nome = userDocument["nome"].ToString(),
                Telefone = userDocument["telefone"].ToString(),
                Password = userDocument["password"].ToString()
                // Adicione outras propriedades conforme necessário
            };
        }

        public List<BsonDocument> GetAllUsers()
        {
            var collection = _database.GetCollection<BsonDocument>("users");
            return collection.Find(new BsonDocument()).ToList(); // Retorna todos os documentos
        }
    }
}
