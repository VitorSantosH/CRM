using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CRM.Entidades
{
    public class Usuario
    {
        [BsonId] // Informa ao MongoDB que esse é o campo _id
        [BsonRepresentation(BsonType.ObjectId)] // Faz a conversão automática para string se necessário
        public ObjectId Id { get; set; }  // ID único gerado pelo MongoDB

        [BsonElement("nome")]
        public string Nome { get; set; }  // Nome do usuário

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("telefone")]
        public string Telefone { get; set; }  // Telefone do usuário

        [BsonElement("cidade")]
        public string Cidade { get; set; }  // Cidade do usuário

        [BsonElement("observacoes")]
        public string Observacoes { get; set; }  // Observações sobre o usuário

        [BsonElement("email")]
        public string Email { get; set; }  // Email do usuário
    }

}
