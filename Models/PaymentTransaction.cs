namespace DataProcessing.Models
{
    public class PaymentTransaction
    {
        public string? City { get; set; }
        public decimal Total { get; set; }
        public List<Service>? Services { get; set; }
    }
}
