using System;
using System.IO;
using SixLabors.ImageSharp;

namespace Converter
{
    public class ImageFormatService 
    {
        public string UpdateFileFormat(string fileLocation, string fileType)
        {
            IImageTypeConverter converter = new ImageConverter();
            return converter.Convert(fileLocation, fileType);
        }
    }
}