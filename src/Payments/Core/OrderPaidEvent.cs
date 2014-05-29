namespace Payments.Core
{
    public class OrderPaidEvent
    {
        public OrderInfo OrderInfo { get; private set; }

        public OrderPaidEvent(OrderInfo orderInfo)
        {
            OrderInfo = orderInfo;
        }
    }
}