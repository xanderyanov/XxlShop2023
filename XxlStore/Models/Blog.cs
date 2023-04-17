using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using static System.Net.Mime.MediaTypeNames;

namespace XxlStore.Models
{
    public class Image
    {
        public string FileName { get; set; }
        public string Alt { get; set; }
        public string Title { get; set; }

    }

    [BsonIgnoreExtraElements]
    public class Post {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public string Text { get; set; }
        
        public string Author { get; set; }

        public Image CoverImage { get; set; } = new();

        public DateTime CreatedDate { get; set; }
        public DateTime PostDate { get; set; }

        public DateTime UpdatedDate { get; set;}

        public bool Published { get; set; }
    }
    public class Blog
    {
        public Post Post { get; set; }
        public List<Post> Posts { get; set;}

    }
}
