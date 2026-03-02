using PaymentService.Models;

namespace PaymentService.Services
{
    public class PaymentStore
    {
        private readonly List<Payment> _payments = new();
        private int _idCounter = 1;

        public List<Payment> GetAll() => _payments;

        public Payment Add(Payment payment)
        {
            payment.Id = _idCounter++;
            payment.Status = "SUCCESS";
            _payments.Add(payment);
            return payment;
        }

        public Payment? GetById(int id)
        {
            return _payments.FirstOrDefault(p => p.Id == id);
        }
    }
}