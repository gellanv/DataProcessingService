using System.Text;

namespace DataProcessing.Services
{
    public static class WriteDataToFileService
    {
        public static void WriteDataToFile(IConfiguration configuration, string jsonString)
        {
            string path = CreateFolderService.CreateFolderIfNotExist(configuration.GetSection("path:folderB").Value!);

            try
            {
                using (FileStream fs = File.OpenWrite($"{path}\\output-{Guid.NewGuid()}.json"))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(jsonString);
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
