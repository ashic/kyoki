namespace Fraud.Core
{
    public class FraudCheckResultViewModel
    {
        public string OrderId { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
        public string Status { get; set; }

        public FraudCheckResultViewModel(string orderId, string email, int score, string status)
        {
            OrderId = orderId;
            Email = email;
            Score = score;
            Status = status;
        }
    }
}