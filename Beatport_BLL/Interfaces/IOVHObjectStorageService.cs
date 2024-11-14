using Beatport_BLL.Dtos.OVH;

namespace Beatport_BLL.Interfaces;

public interface IOVHObjectStorageService
{
    public Task UploadFileAsync(UploadFileDto uploadFileDto);
}