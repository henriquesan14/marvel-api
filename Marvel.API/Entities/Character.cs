namespace Marvel.API.Entities
{
    public class Character
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Modified { get; set; }
        public string ResourceURI { get; set; }
        public bool IsFavorite { get; set; }
        public virtual Thumbnail Thumbnail { get; set; }
        public string ImageUrl
        {
            get
            {
                return ($"{Thumbnail?.Path}.{Thumbnail?.Extension}") ?? string.Empty;
            }
            set
            {
                
            }
        }
    }
}
