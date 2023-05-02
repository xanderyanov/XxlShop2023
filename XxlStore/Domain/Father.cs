using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Xml.Serialization;

namespace XxlStore
{
    public class Father
    {
        [XmlIgnore]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonIgnore]
        public string IdAsString
        {
            get
            {
                return Id.ToString();
            }
            set
            {
                if (value == null)
                    Id = ObjectId.Empty;
                else
                    Id = new ObjectId(value);
            }
        }
    }
}
