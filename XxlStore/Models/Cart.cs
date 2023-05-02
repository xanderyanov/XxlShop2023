using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace XxlStore.Models
{


    [BsonIgnoreExtraElements]  //если какого-то поля нет - не выведет ошибку а проигнорирует
    public class Cart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public List<Product> products { get; set; }

        public bool IsActive { get; set; }

    }
}
