using CloudinaryDotNet.Actions;

namespace API.Interfaces.Services
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file,bool isProfile);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}