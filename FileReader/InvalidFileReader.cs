using DataProcessing.FileReader.Interface;
using DataProcessing.Models;

namespace DataProcessing.FileReader
{
    public class InvalidFileReader : IFileReader
    {
        public Task Read(string filePath, int numFile, IConfiguration configuration)
        {
            string path = configuration.GetSection("path:folderA").Value + "\\invalid\\";

            Directory.CreateDirectory(path);

            string newPath = path + numFile+Path.GetFileName(filePath);
            try
            {
                if (File.Exists(filePath))
                {
                    File.Move(filePath, newPath);
                    MetaData.Invalid_files.Add(filePath);
                    return Task.CompletedTask;
                }
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromException(ex);
            }
        }
    }
}
