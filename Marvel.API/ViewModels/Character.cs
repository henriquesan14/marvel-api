namespace Marvel.API.ViewModels
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Modified { get; set; }
        public string ResourceURI { get; set; }
        public string DescriptionURI { get; set; }
        
        public Thumbnail Thumbnail { get; set; }
        public bool IsFavorite { get; set; } = false;
        public string ImageUrl {
            get
            {
                return ($"{Thumbnail?.Path}.{Thumbnail?.Extension}") ?? string.Empty;
            }
            set
            {
                ImageUrl = value;
            }
        }
    }
}
