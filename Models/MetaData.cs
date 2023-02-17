using System.Text;

namespace DataProcessing.Models
{
    public static class MetaData
    {
        public static int Parsed_files { get; set; }
        public static int Parsed_lines { get; set; }
        public static int Found_errors { get; set; }

        public static Queue<int> QueueNumberFile { get; set; }
        public static List<string> Invalid_files { get; set; } = new List<string>();

        public static string WriteToString()
        {
            StringBuilder filesList = new StringBuilder("");
            foreach (var f in Invalid_files)
            {
                filesList.Append(f + ", ");
            }

            return $"parsed_files: {Parsed_files}\nparsed_lines: {Parsed_lines}\nfound_errors: {Found_errors}\ninvalid_files: [{filesList.ToString().TrimEnd(new char[] { ',', ' ' })}]";
        }
    }
}
