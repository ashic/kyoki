namespace Till.Core
{
    public class PlaceOrderCommand
    {
        public OrderDetails Order { get; private set; }

        public PlaceOrderCommand(OrderDetails order)
        {
            Order = order;
        }
    }
}