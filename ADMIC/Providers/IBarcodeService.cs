using System;
using System.IO;

namespace ADMIC.Providers
{
    public interface IBarcodeService
    {
        Stream ConvertImageStream(string text, int width = 500, int height = 500);
    }
}
