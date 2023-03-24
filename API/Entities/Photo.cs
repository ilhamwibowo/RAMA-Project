using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Photo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PhotoId { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }

        //for bibtag

        public List<string> BibTags { get; set; }= new List<string>();
        
    }
}