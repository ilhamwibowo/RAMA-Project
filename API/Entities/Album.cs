using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Album
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AlbumId{ get; set; }

        public string AlbumName{ get; set; }

        public List<Photo> AlbumPhotos{ get; set; } = new List<Photo>();
    }
}