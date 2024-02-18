using System.Text.Json.Serialization;

namespace Marvel.API.Entities
{
    public class Thumbnail
    {
        public long Id { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
        [JsonIgnore]
        public virtual Character Character { get; set; }
    }
}
