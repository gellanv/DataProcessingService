using DataProcessing.FileReader;
using DataProcessing.Models;

namespace DataProcessingService.Services
{
    public class ProcessingFilesService
    {
        protected readonly IConfiguration _configuration;
        public ProcessingFilesService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task ProcessingFiles(string[] files)
        {
            Parallel.ForEach(files, async file =>
            {
                int num = ++MetaData.TodayCurrentNumber;

                FileModel fileModel;
                string extension = Path.GetExtension(file);

                if (extension == ".txt")
                {
                    fileModel = new FileModel(file, new ValidFileReaderTxt(), _configuration, num);
                    await fileModel.Read();
                }
                else if (extension == ".csv")
                {

                    fileModel = new FileModel(file, new ValidFileReaderCsv(), _configuration, num);
                    await fileModel.Read();
                }
                else
                {
                    fileModel = new FileModel(file, new InvalidFileReader(), _configuration, num);
                    await fileModel.Read();
                }

            });
        }

    }
}
