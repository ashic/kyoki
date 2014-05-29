namespace Payments.Core
{
    public class PaymentsViewModel
    {
        public string OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Status { get; set; }
        public string PaymentReference { get; set; }
        public string Provider { get; set; }

        public PaymentsViewModel(string orderId, decimal amount, string currency, string status, string paymentReference, string provider)
        {
            OrderId = orderId;
            Amount = amount;
            Currency = currency;
            Status = status;
            PaymentReference = paymentReference;
            Provider = provider;
        }
    }
}