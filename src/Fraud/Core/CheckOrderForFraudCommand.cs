namespace Fraud.Core
{
    public class CheckOrderForFraudCommand
    {
        public OrderInfo Order { get; private set; }

        public CheckOrderForFraudCommand(OrderInfo order)
        {
            Order = order;
        }
    }
}