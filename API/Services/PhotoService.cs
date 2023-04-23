using API.Data;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Google.Cloud.Vision.V1;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(acc);
        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                    Folder = "RAMA"
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }
        public async Task<ImageUploadResult> AddPhotoAlbumAsync(IFormFile file, string AlbumName)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "RAMA/" + AlbumName
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }
        public async Task<List<string>> GetText(string Url)
        {
            var client = ImageAnnotatorClient.Create();
            var image = Image.FromUri(Url);
            var response = await client.DetectTextAsync(image);
            //TODO: Filtering only bib numbers?
            List<string> words = new List<string>();
            foreach(var annotation  in response)
            {
                if (annotation.Description != null)
                {
                    words.Add(annotation.Description);
                }
            }
            return words;
        }
        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            return  await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}