namespace Dispatch.Core
{
    public class DispatchOrderCommand
    {
        public OrderInfo Order { get; private set; }

        public DispatchOrderCommand(OrderInfo order)
        {
            Order = order;
        }
    }
}