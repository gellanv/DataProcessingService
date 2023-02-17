using DataProcessing.FileReader.Interface;
using DataProcessing.Models;
using DataProcessing.Services;
using System.Text.Json;

namespace DataProcessing.FileReader
{
    public class ValidFileReaderCsv : IFileReader
    {
        public Task Read(string filePath, IConfiguration configuration)
        {
            List<string> LinesPart = ReadFileService.ReadFile(filePath, "first_name:");

            FileInfo fileInfo = new FileInfo(filePath);
            fileInfo.Delete();

            List<PaymentTransaction> listObject = TransformLinesService.transformLinesToPaymentTransaction(LinesPart);

            try
            {
                string jsonString = JsonSerializer.Serialize(listObject);
                WriteDataToFileService.WriteDataToFile(configuration, jsonString);
                MetaData.Parsed_files++;
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
