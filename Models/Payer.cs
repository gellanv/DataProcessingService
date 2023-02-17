namespace DataProcessing.Models
{
    public class Payer
    {
        public string? Name { get; set; }
        public decimal Payment { get; set; }
        public string? Date { get; set; }
        public long Account_number { get; set; }
    }
}
