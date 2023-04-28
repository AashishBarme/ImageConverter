using System;
using System.IO;
using SixLabors.ImageSharp;
namespace Converter
{
    public class WebPConverter: IImageTypeConverter
    {
        public string Convert(string fileLocation)
        {
             var fileExtension = Path.GetExtension(fileLocation);
            if(fileExtension == ".webp")
            {
                return fileLocation;
            }
            var fileName = Path.GetFileNameWithoutExtension(fileLocation);
            var uploadDir = Directory.GetParent(fileLocation).FullName;
            string uploadFullPath = $"{uploadDir}/{fileName}.webp";
            try
            {
                using (Image image = Image.Load(fileLocation))
                {
                    image.SaveAsWebp(uploadFullPath);
                }
                DeleteOldImageAfterConversion(fileLocation);
                return uploadFullPath;
            }  catch(SixLabors.ImageSharp.UnknownImageFormatException e)
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