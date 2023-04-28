namespace Console;
using System;
using Converter;
using Microsoft.Extensions.Configuration;
public  class ImageConverter
{
    // private readonly ImageFormatService _formatService;
    // public ImageConverter(ImageFormatService formatService)
    // {
    //     _formatService = formatService;
    // }
    public void Convert(IConfigurationRoot configuration)
    {
        var formatService =  new ImageFormatService();
        // Console.WriteLine(configuration["FilePath"]);
        DirectoryInfo dInfo = new DirectoryInfo(configuration["filePath"]);
        FileInfo[] files = dInfo.GetFiles();

        foreach(var item in files)
        {
            Console.WriteLine(item.FullName);
            Console.WriteLine(item.Directory.ToString());
            formatService.UpdateFileFormatIntoPng(item.FullName.ToString());
        }
    } 
}