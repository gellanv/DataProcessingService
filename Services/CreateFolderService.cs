namespace DataProcessing.Services
{
    public static class CreateFolderService
    {
        public static string CreateFolderIfNotExist(string pathFolder)
        {
            DateTime dateTime = DateTime.Now;
            string nameFolder = $"{dateTime.Month}-{dateTime.Day}-{dateTime.Year}";
            string path = pathFolder + "\\" + nameFolder;

            var directory = new DirectoryInfo(path);
            if (!directory.Exists)
            {
                directory.Create();
            }
            return path;
        }
    }
}
