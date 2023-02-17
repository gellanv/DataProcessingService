namespace DataProcessing.Models
{
    public class Service
    {
        public string? Name { get; set; }
        public decimal Total { get; set; }
        public List<Payer>? Payers { get; set; }
    }
}
