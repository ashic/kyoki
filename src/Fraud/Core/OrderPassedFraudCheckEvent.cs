namespace Fraud.Core
{
    public class OrderPassedFraudCheckEvent
    {
        public OrderInfo OrderInfo { get; private set; }

        public OrderPassedFraudCheckEvent(OrderInfo orderInfo)
        {
            OrderInfo = orderInfo;
        }
    }
}