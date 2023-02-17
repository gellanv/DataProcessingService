using DataProcessing.Models;
using System.Text;

namespace DataProcessing.Services
{
    public static class WriteMetaDataService
    {
        public static void WriteMetaDataToFile(IConfiguration _configuration)
        {
            string path = CreateFolderService.CreateFolderIfNotExist(_configuration.GetSection("path:folderB").Value!);
            string pathMetaFile = path + "\\meta.log";
            try
            {
                using (FileStream fs = File.Create(pathMetaFile))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(MetaData.WriteToString());
                    fs.Write(info, 0, info.Length);
                }
                MetaData.Found_errors = 0;
                MetaData.Parsed_lines = 0;
                MetaData.Parsed_files = 0;
                MetaData.Invalid_files = new List<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
