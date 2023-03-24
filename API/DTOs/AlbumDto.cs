namespace API.DTOs
{
    public class AlbumDto
    {
        public Guid AlbumId { get; set; }

        public string AlbumName { get; set; }

        public List<PhotoDto> AlbumPhotos { get; set; }
    }
}