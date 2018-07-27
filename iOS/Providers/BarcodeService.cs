﻿using System;
using System.IO;
using ADMIC.iOS.Providers;
using ADMIC.Providers;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(BarcodeService))]
namespace ADMIC.iOS.Providers
{
    public class BarcodeService : IBarcodeService
    {

        public Stream ConvertImageStream(string text, int width = 300, int height = 300)
        {
            var xx = (float)UIScreen.MainScreen.Bounds.Width;
            int yy = 500;
            if (xx == 320.0)
            {
                yy = (int)(xx * 0.75);
            }

            var barcodeWriter = new ZXing.Mobile.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = yy,
                    Height = yy,
                    Margin = 1
                }
            };
            barcodeWriter.Renderer = new ZXing.Mobile.BitmapRenderer();

            var bitmap = barcodeWriter.Write(text);
            var stream = bitmap.AsPNG().AsStream(); // this is the difference 
            stream.Position = 0;

            return stream;
        }

    }
}