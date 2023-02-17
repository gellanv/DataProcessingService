namespace DataProcessing.Services
{
    public static class ReadFileService
    {
        public static List<string> ReadFile(string path, string? splitStr)
        {
            List<string> Lines = new List<string>();
            if (File.Exists(path))
            {
                try
                {
                    using (StreamReader stream = new StreamReader(path))
                    {
                        while (stream.EndOfStream != true)
                        {
                            string line = stream.ReadLine();
                            if (line != null && splitStr != null && line.Contains(splitStr))
                                continue;
                            else
                            {
                                Lines.Add(line);
                            }
                        }
                        stream.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return Lines;
        }
    }
}
