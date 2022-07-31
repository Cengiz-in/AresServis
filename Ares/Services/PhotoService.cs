using API.Helpers;
using API.Interfaces.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        private readonly string _folder;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _folder = config.Value.Folder;
            _cloudinary = new Cloudinary(acc);
        }
        
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file,bool isProfile)
        {
            var uploadResult = new ImageUploadResult();

            if(file.Length > 0){
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams{
                    File = new FileDescription(file.FileName,stream),
                    Folder = _folder
                };
                if(isProfile) uploadParams.Transformation =  new Transformation().Height(500).Width(500).Crop("fill").Gravity("face");
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;

        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result;
        }
    }
}