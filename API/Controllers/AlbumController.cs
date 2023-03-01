using API.Data;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class AlbumController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IPhotoService _photoService;
        public AlbumController(DataContext context, ILogger<AccountController> logger, IPhotoService photoService)
        {
            _photoService = photoService;
            _context = context;
            _logger = logger;
        } 
        [HttpPost]
        public async Task<ActionResult> AddPhoto(int albumId, IFormFile file)
        {   
            Account requester = _context.Accounts.FirstOrDefault(x => x.AccId == User.GetUserId());
            // if (requester.Role != "Admin") Unauthorized("No Permission!");
            if (!isImage(file.FileName)) return BadRequest("File must be an Image");

            Album album = _context.Albums.FirstOrDefault(x => x.AlbumId == albumId);
            if (album == null) return BadRequest("Album Not Found");
            var result = await _photoService.AddPhototoAlbumAsync(file, album.AlbumName);

            if (result.Error != null) return BadRequest(result.Error.Message); 
            List<string> stringtag = await _photoService.GetText(result.SecureUrl.AbsoluteUri);
            stringtag.RemoveAt(0);
            //remove bcs the first index isinya semua tag yang digabungin dengan "\n", tidak dibutuhkan
            Photo photo = new Photo
                {
                    Url = result.SecureUrl.AbsoluteUri,
                    PublicId = result.PublicId,
                    BibTags = stringtag
                };
            
            album.AlbumPhotos.Add(photo);
            _context.Albums.Update(album);
            _context.SaveChangesAsync();
            return Ok();
        }
        private bool isImage(string filename){
            string[] ext = {".jpg",".bmp",".gif",".png"};
            return ext.Any(x => filename.EndsWith(x));
        }
    }
}