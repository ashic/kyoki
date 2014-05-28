namespace Till.Core
{
    public class OrderPlacedEvent
    {
        public OrderDetails Order { get; private set; }

        public OrderPlacedEvent(OrderDetails order)
        {
            Order = order;
        }
    }
}