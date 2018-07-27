using System;
using System.Threading.Tasks;

namespace ADMIC.Providers
{
    public interface IScreenshotManager
    {
        Task<byte[]> CaptureAsync();
    }
}