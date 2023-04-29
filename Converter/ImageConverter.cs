using System;
using System.IO;
using System.Transactions;
using ImageMagick;
using SixLabors.ImageSharp;
namespace Converter
{
    public class ImageConverter: IImageTypeConverter
    {
        public string Convert(string fileLocation, string fileType)
        {
             var fileExtension = Path.GetExtension(fileLocation);
            if(fileExtension == $".{fileType}")
            {
                return fileLocation;
            }
            var fileName = Path.GetFileNameWithoutExtension(fileLocation);
            var uploadDir = Directory.GetParent(fileLocation)?.FullName;
            string uploadFullPath = $"{uploadDir}/{fileName}{fileExtension}";
            try
            {
                using (var image = new MagickImage(uploadFullPath))
                {
                    image.Write($"{uploadDir}/{fileName}.{fileType}");
                }

                // DeleteOldImageAfterConversion(fileLocation);
                return uploadFullPath;
            }  catch(MagickException  e)
            {
                Console.WriteLine(e.Message);
                return fileLocation;
            }
        }



        public byte[] Convert(Stream fileLocation, string fileFormat)
        {
            try
            {
                using (var memStream = new MemoryStream())
                {
                    using (var image = new MagickImage(fileLocation))
                    {
                        image.Format = GetMagickFormat(fileFormat);
                        image.Write(memStream);
                        return memStream.ToArray();
                    }
                }
            }  catch(MagickException  e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private static MagickFormat GetMagickFormat(string format)
        {
            return format switch
            {
                "png" => MagickFormat.Png,
                "jpg" => MagickFormat.Jpg,
                "webp" => MagickFormat.WebP,
                "tiff" => MagickFormat.Tiff,
                "ico" => MagickFormat.Ico,
                "gif" => MagickFormat.Gif,
                _ => MagickFormat.Jpeg,
            };
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