using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetAlbums(int id, string query = null)
        {
            if (query == null) query = string.Empty;

            List<String> queryList = query.ToLower().Split(new char[] { ',', ' ', '\n', ';' }).ToList();
            var album = await _context.Albums.Include(x => x.AlbumPhotos).FirstOrDefaultAsync(a => a.AlbumId == id);
            // return Ok(album);
            var photos = album.AlbumPhotos;
            if (query != string.Empty)
                photos = photos.FindAll(photo => queryList.Any(q => photo.BibTags.Contains(q)));
            var photoDtos = new List<PhotoDto>();
            foreach (var photo in photos)
            {
                photoDtos.Add(new PhotoDto
                {
                    Id = photo.PhotoId,
                    Url = photo.Url,

                });
            }
            return Ok(
                new AlbumDto
                {
                    albumId = album.AlbumId,
                    albumName = album.AlbumName,
                    Photos = photoDtos,

                }
            );

        }

        [HttpDelete("photos/{photoId}")]
        public async Task<IActionResult> DeletePhoto(int photoId)
        {
            // Find photo
            var photo = await _context.Photo.FindAsync(photoId);
            if (photo == null) return NotFound();

            // Remove photo from cloud
            var result = await _photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null) return BadRequest(result.Error.Message);

            // Remove photo from table
            _context.Remove(photo);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        private bool isImage(string filename)
        {
            string[] ext = { ".jpg", ".bmp", ".gif", ".png" };
            return ext.Any(x => filename.EndsWith(x));
        }
    }
}