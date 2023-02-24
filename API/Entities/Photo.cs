using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }

        //for bibtag

        public List<int> BigTags { get; set; }= new List<int>();
        
    }
}