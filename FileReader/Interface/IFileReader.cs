using Microsoft.Extensions.Configuration;

namespace DataProcessing.FileReader.Interface
{
    public interface IFileReader
    {
       Task Read(string filePath, /*int NumberFile,*/ IConfiguration configuration);
    }
}
