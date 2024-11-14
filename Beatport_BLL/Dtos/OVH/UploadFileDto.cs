namespace Beatport_BLL.Dtos.OVH;

public class UploadFileDto
{
    public Stream fileStream { get; set; }
    public string contentType { get; set; }
    public string objectKey { get; set; }
}