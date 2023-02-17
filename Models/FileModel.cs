using DataProcessing.FileReader.Interface;
using Microsoft.Extensions.Configuration;

namespace DataProcessing.Models
{
    public class FileModel
    {
        protected string FilePath { get; set; }
        public IFileReader FileReader { private get; set; }
        public int NumberFile { get; set; }

        private readonly IConfiguration configuration;

        public FileModel(string path, int numberFile, IFileReader reader, IConfiguration configuration)
        {
            this.FilePath = path;
            this.NumberFile = numberFile;
            this.FileReader = reader;
            this.configuration = configuration;
        }
        public void Read()
        {
            FileReader.Read(FilePath, NumberFile, configuration);
        }
    }
}
