using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.IServices
{
    public interface IStorageServices
    {
        Task<APIResponse> DeleteFile(string path);
        Task<ServiceResponse<string>> UploadFile(string path, IFormFile file);
        Task<ServiceResponse<string>> UploadFile(string path, string file);
    }
}