namespace API.DTOs
{
    public class AlbumDto
    {
        public int albumId { get; set; }

        public string albumName { get; set; }

        public List<PhotoDto> AlbumPhotos { get; set; }
    }
}