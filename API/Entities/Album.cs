using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Album
    {
        [Key]
        public int AlbumId{ get; set; }

        public string AlbumName{ get; set; }

        public List<Photo> AlbumPhotos{ get; set; } = new List<Photo>();
    }
}