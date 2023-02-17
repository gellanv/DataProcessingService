using DataProcessing.FileReader;
using DataProcessing.Models;

namespace DataProcessingService.Services
{
    public class ProcessingOneFileService
    {
        private readonly IConfiguration _configuration;
        public ProcessingOneFileService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task ProcessingOneFile(string file)
        {
            FileModel fileModel;
            string extension = Path.GetExtension(file);

            if (extension == ".txt")
            {
                fileModel = new FileModel(file, new ValidFileReaderTxt(), _configuration);
                await fileModel.Read();
            }
            else if (extension == ".csv")
            {
                fileModel = new FileModel(file, new ValidFileReaderCsv(), _configuration);
                await fileModel.Read();
            }
            else
            {
                fileModel = new FileModel(file, new InvalidFileReader(), _configuration);
                await fileModel.Read();
            }

        }
    }
}
