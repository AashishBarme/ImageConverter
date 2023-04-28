using System;
using System.IO;
using SixLabors.ImageSharp;

namespace Converter
{
    public class ImageFormatService 
    {
        public string UpdateFileFormatIntoWebp(string fileLocation)
        {
            IImageTypeConverter converter = new WebPConverter();
            return converter.Convert(fileLocation);
        }
    }
}