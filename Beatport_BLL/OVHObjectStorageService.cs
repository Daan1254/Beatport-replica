using Amazon;
using Amazon.Internal;
using Amazon.S3;
using Amazon.S3.Model;
using Beatport_BLL.Dtos.OVH;
using Beatport_BLL.Exceptions;
using Beatport_BLL.Interfaces;
using dotenv.net;

namespace Beatport_BLL;

public class OVHObjectStorageService : IOVHObjectStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    
    public OVHObjectStorageService()
    {
        string accessKey = DotEnv.Read()["OVH_ACCESS_KEY"];
        string secretKey = DotEnv.Read()["OVH_SECRET_KEY"];
        string serviceUrl = DotEnv.Read()["OVH_ENDPOINT"];
        _bucketName = DotEnv.Read()["OVH_CONTAINER_NAME"];
        _s3Client = new AmazonS3Client(
            accessKey,
            secretKey,
            new AmazonS3Config
            {
                ServiceURL = serviceUrl,
                ForcePathStyle = true, // OVH requires this setting
                RegionEndpoint = RegionEndpoint.EUCentral1,
            });
    }
    
    
    public async Task UploadFileAsync(UploadFileDto uploadFileDto)
    {
        try
        {
            if (uploadFileDto.contentType == null)
            {
                throw new InvalidContentTypeException("Content type cannot be null.");
            }
            
            if (!uploadFileDto.contentType.Equals("audio/wav") && !uploadFileDto.contentType.Equals("audio/mp3") && !uploadFileDto.contentType.Equals("audio/mpeg"))
            {
                throw new InvalidContentTypeException($"Invalid content type only wav and mp3 files are allowed. found {uploadFileDto.contentType}");
            }
            
            
            var uploadRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = uploadFileDto.objectKey,
                InputStream = uploadFileDto.fileStream,
                ContentType = uploadFileDto.contentType,
            };

            var response = await _s3Client.PutObjectAsync(uploadRequest);

            Console.WriteLine($"File uploaded successfully. Status Code: {response.HttpStatusCode}");
        }
        catch (Exception ex)
        {
            throw new BadRequestException(ex.Message, ex);
        }
    }
}