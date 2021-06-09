using FluentPOS.Shared.DTOs.Upload;

namespace FluentPOS.Shared.Abstractions.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}
