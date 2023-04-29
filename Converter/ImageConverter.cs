using System;
using System.IO;
using ImageMagick;
using SixLabors.ImageSharp;
namespace Converter
{
    public class ImageConverter: IImageTypeConverter
    {
        public string Convert(string fileLocation, string fileType)
        {
             var fileExtension = Path.GetExtension(fileLocation);
            if(fileExtension == ".png")
            {
                return fileLocation;
            }
            var fileName = Path.GetFileNameWithoutExtension(fileLocation);
            var uploadDir = Directory.GetParent(fileLocation).FullName;
            string uploadFullPath = $"{uploadDir}/{fileName}{fileExtension}";
            try
            {
                // Read first frame of gif image
                using (var image = new MagickImage(uploadFullPath))
                {
                    // Save frame as jpg
                    image.Write($"{uploadDir}/{fileName}.png");
                }

                // DeleteOldImageAfterConversion(fileLocation);
                return uploadFullPath;
            }  catch(MagickException  e)
            {
                Console.WriteLine(e.Message);
                return fileLocation;
            }
        }
         private void DeleteOldImageAfterConversion(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }
            File.Delete(path);
        }
    }
}