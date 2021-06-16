using System.Threading.Tasks;
using FluentPOS.Shared.DTOs.Upload;

namespace FluentPOS.Shared.Application.Interfaces.Services
{
    public interface IUploadService
    {
        Task<string> UploadAsync(UploadRequest request);
    }
}