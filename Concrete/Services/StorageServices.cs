using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Global;
using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.Services
{
    public class StorageServices : IStorageServices
    {
        private readonly IConfiguration _configuration;
        private readonly IFileExtensionService _fileExtensionService;
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APSouth1;

        public StorageServices(IConfiguration configuration, IFileExtensionService fileExtensionService)
        {
            _configuration = configuration;
            _fileExtensionService = fileExtensionService;
        }

        private static int MAX_FILE_SIZE = 1024 * 1024 * 70; // 70 MB
        private static string[] EXTENSION_LOWER_CASE = new string[] { "pdf", "jpg", "png", "jpeg", "gif", "mp4", "webp" };
        private static string[] IMG_EXTENSION_LOWER_CASE = new string[] { "jpg", "png", "jpeg", "gif", "webp" };

        private string BaseUrl = "";

        private string GenerateS3Path(string path, string fileName)
        {
            var s = $"{path}/{Guid.NewGuid()}{Path.GetExtension(fileName).ToLower()}";
            return s;
        }

        private bool ValidateFilesSize(IFormFile file)
        {
            return (file == null) ? false : (file.Length > MAX_FILE_SIZE) ? false : true;
        }
        private string GetFileExtension(IFormFile file)
        {
            return (file == null) ? "" : Path.GetExtension(file.FileName).ToLower();
        }

        private bool IsExtensionsAvailable(string extension)
        {
            return EXTENSION_LOWER_CASE.Contains(extension) ? false : true;
        }


        public async Task<ServiceResponse<string>> UploadFile(string path, IFormFile file)
        {
            try
            {
                using IAmazonS3 client = new AmazonS3Client(_configuration.GetValue<string>("AWS:accessKey"), _configuration.GetValue<string>("AWS:secretKey"), bucketRegion);
                if (file == null)
                    return new ServiceResponse<string>("File was null", StatusCodes.Status400BadRequest, null);

                if (ValidateFilesSize(file) == false)
                    return new ServiceResponse<string>("Entity Too Large", StatusCodes.Status413RequestEntityTooLarge, null);

                if (IsExtensionsAvailable(file.FileName.Split(".")[1]))
                    return new ServiceResponse<string>("Not Acceptable", StatusCodes.Status406NotAcceptable, null);

                var filetransferutility = new TransferUtility(client);
                var filePath = GenerateS3Path(path, file.FileName);
                using (var fileStream = file.OpenReadStream())
                {
                    await filetransferutility.UploadAsync(fileStream, _configuration.GetValue<string>("AWS:bucketName"), filePath);
                }
                await client.PutACLAsync(new PutACLRequest
                {
                    BucketName = _configuration.GetValue<string>("AWS:bucketName"),
                    CannedACL = S3CannedACL.PublicRead,
                    Key = filePath
                });
                return new ServiceResponse<string>("", StatusCodes.Status200OK, $"{BaseUrl}{filePath}");

            }
            catch (AmazonS3Exception e)
            {
                return new ServiceResponse<string>(e.Message, StatusCodes.Status500InternalServerError, null);
            }
            catch (Exception e)
            {
                return new ServiceResponse<string>(e.Message, StatusCodes.Status500InternalServerError, null);
            }
        }


        public async Task<ServiceResponse<string>> UploadFile(string path, string file)
        {
            try
            {
                if (file.StartsWith("http"))
                {
                    return new(ResponseConstants.Success, 200, file);
                }
                var extension = _fileExtensionService.GetExtension(file[..5]);

                //if (IMG_EXTENSION_LOWER_CASE.Contains(extension[1..]))
                //{
                //    //var res = await GetCompressedImageAsync(file, 300000);
                //    file = res;
                //}
                byte[] inBytes = Convert.FromBase64String(file);
                var stream = new MemoryStream(inBytes);
                Random random = new Random();
                var fileName = random.Next(100000, 199999).ToString();

                using IAmazonS3 client = new AmazonS3Client
                    (_configuration.GetValue<string>("AWS:accessKey"), _configuration.GetValue<string>("AWS:secretKey"), bucketRegion);
                var filetransferutility = new TransferUtility(client);


                var filePath = GenerateS3Path(path, $"{fileName}{extension}");
                await filetransferutility.UploadAsync(stream, _configuration.GetValue<string>("AWS:bucketName"), filePath);
                await client.PutACLAsync(new PutACLRequest
                {
                    BucketName = _configuration.GetValue<string>("AWS:bucketName"),
                    CannedACL = S3CannedACL.PublicRead,
                    Key = filePath
                });
                return new ServiceResponse<string>("Success", StatusCodes.Status200OK, $"{BaseUrl}{filePath}");
            }
            catch (AmazonS3Exception e)
            {
                return new ServiceResponse<string>(e.Message, StatusCodes.Status500InternalServerError, null);
            }
            catch (Exception e)
            {
                return new ServiceResponse<string>(e.Message, StatusCodes.Status500InternalServerError, null);
            }
        }


        //private async Task<string> GetCompressedImageAsync(string base64Image, long targetSizeInBytes)
        //{
        //    byte[] imageBytes = Convert.FromBase64String(base64Image);
        //    using MemoryStream inStream = new(imageBytes);
        //    using MemoryStream outStream = new();

        //    using (Image image = await Image.LoadAsync(inStream))
        //    {
        //        int quality = 100;
        //        var encoder = new JpegEncoder { Quality = quality };

        //        do
        //        {
        //            outStream.Seek(0, SeekOrigin.Begin);
        //            outStream.SetLength(0);

        //            image.Save(outStream, encoder);

        //            byte[] compressedBytes = outStream.ToArray();
        //            if (compressedBytes.Length <= targetSizeInBytes)
        //            {
        //                break;
        //            }

        //            quality -= 5;
        //            encoder = new JpegEncoder { Quality = quality };

        //            if (quality <= 5)
        //            {
        //                break;
        //                //throw new Exception("Compression failed to achieve target size.");
        //            }
        //        }
        //        while (true);
        //    }

        //    return Convert.ToBase64String(outStream.ToArray());
        //}


        public async Task<APIResponse> DeleteFile(string path)
        {
            try
            {
                if (path is null)
                    return new APIResponse("Path cannot be null", 400);

                var keyFileName = string.Empty;
                var splitPath = path.Split("/");
                for (int i = 0; i < splitPath.Length; i++)
                {
                    if (splitPath[i].EndsWith(".jpeg") || splitPath[i].EndsWith(".jpg") || splitPath[i].EndsWith(".pdf") || splitPath[i].EndsWith(".png"))
                        keyFileName = $"{_configuration.GetValue<string>("AWS:subFolder")}/{splitPath[i]}";
                }
                using IAmazonS3 client = new AmazonS3Client(_configuration.GetValue<string>("AWS:accessKey"),
                    _configuration.GetValue<string>("AWS:secretKey"), bucketRegion);
                DeleteObjectRequest request = new()
                {
                    BucketName = _configuration.GetValue<string>("AWS:bucketName"),
                    Key = keyFileName
                };
                var abc = await client.DeleteObjectAsync(request);
                return new APIResponse("Successfully Removed", 200);
            }
            catch
            {
                return new APIResponse("failed", 400);
            }
        }
    }
}
