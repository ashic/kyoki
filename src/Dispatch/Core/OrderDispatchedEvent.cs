namespace Dispatch.Core
{
    public class OrderDispatchedEvent
    {
        public OrderInfo OrderInfo { get; private set; }

        public OrderDispatchedEvent(OrderInfo orderInfo)
        {
            OrderInfo = orderInfo;
        }
    }
}