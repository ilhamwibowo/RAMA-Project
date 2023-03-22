using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using API.Services;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public AlbumController(DataContext context, ILogger<AccountController> logger, IPhotoService photoService, IMapper mapper)
        {
            _photoService = photoService;
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpPost("{albumId}")]
        public async Task<ActionResult> AddPhoto(int albumId, IFormFile file)
        {
            var requester = await _context.Accounts.Select(a => new { a.AccId, a.Role }).FirstOrDefaultAsync(x => x.AccId == User.GetUserId());

            if (requester == null || requester.Role != "Admin") Unauthorized("No Permission!");

            if (!isImage(file.FileName)) return BadRequest("File must be an Image");

            Album album = _context.Albums.FirstOrDefault(x => x.AlbumId == albumId);

            if (album == null) return BadRequest("Album Not Found");
            
            var result = await _photoService.AddPhototoAlbumAsync(file, album.AlbumName);

            if (result.Error != null) return BadRequest(result.Error.Message);

            List<string> stringtag = await _photoService.GetText(result.SecureUrl.AbsoluteUri);

            if (stringtag.Count > 0) stringtag.RemoveAt(0);
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
            return CreatedAtAction(nameof(GetAlbums), new {id = albumId}, _mapper.Map<PhotoDto>(photo));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAlbums(int id, string query = null)
        {
            if (query == null) query = string.Empty;

            List<String> queryList = query.ToLower().Split(new char[] { ',', ' ', '\n', ';' }).ToList();
            var album = await _context.Albums.Include(x => x.AlbumPhotos).FirstOrDefaultAsync(a => a.AlbumId == id);
            
            return Ok(_mapper.Map<AlbumDto>(album));

        }

        [HttpDelete("photos/{photoId}")]
        public async Task<IActionResult> DeletePhoto(int photoId)
        {
            var requester = await _context.Accounts.Select(a => new { a.AccId, a.Role }).FirstOrDefaultAsync(x => x.AccId == User.GetUserId());

            if (requester == null || requester.Role != "Admin") Unauthorized("No Permission!");

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