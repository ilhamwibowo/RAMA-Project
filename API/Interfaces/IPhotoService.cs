using CloudinaryDotNet.Actions;

namespace API.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<ImageUploadResult> AddPhototoAlbumAsync(IFormFile file, string AlbumName);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
        Task<List<string>> GetText(string Url);
    }
}