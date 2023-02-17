using DataProcessing.FileReader.Interface;

namespace DataProcessing.Models
{
    public class FileModel
    {
        protected string FilePath { get; set; }
        public int numFile { get; set; }
        public IFileReader FileReader { private get; set; }

        private readonly IConfiguration configuration;

        public FileModel(string path, IFileReader reader, IConfiguration configuration, int numFile)
        {
            this.FilePath = path;
            this.FileReader = reader;
            this.configuration = configuration;
            this.numFile = numFile;
        }
        public async Task Read()
        {
            await FileReader.Read(FilePath, numFile, configuration);
        }
    }
}
