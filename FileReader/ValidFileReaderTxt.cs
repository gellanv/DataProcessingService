using DataProcessing.FileReader.Interface;
using DataProcessing.Models;
using DataProcessing.Services;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text.Json;

namespace DataProcessing.FileReader
{
    public class ValidFileReaderTxt : IFileReader
    {
        public Task Read(string filePath, int numFile, IConfiguration configuration)
        {           
            List<string> LinesPart = ReadFileService.ReadFile(filePath,null);
            FileInfo fileInfo = new FileInfo(filePath);
            fileInfo.Delete();

            List<PaymentTransaction> listObject = TransformLinesService.transformLinesToPaymentTransaction(LinesPart);

            try
            {                
                string jsonString = JsonSerializer.Serialize(listObject);
                WriteDataToFileService.WriteDataToFile(configuration, jsonString, numFile);
                MetaData.Parsed_files++;
                return Task.CompletedTask;

            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
                return Task.FromException(ex);
            }
        }
    }
}
