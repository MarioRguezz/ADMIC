using System;
namespace ADMIC.Providers
{
    public interface IPicture
    {
        void SavePictureToDisk(string filename, byte[] imageData);
    }
}