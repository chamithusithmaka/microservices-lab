namespace PaymentService.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public double Amount { get; set; }
        public string Method { get; set; } = string.Empty;
        public string Status { get; set; } = "SUCCESS";
    }
}