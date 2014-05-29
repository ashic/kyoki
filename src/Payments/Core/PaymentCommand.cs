namespace Payments.Core
{
    public class PaymentCommand
    {
        public OrderInfo Order { get; private set; }

        public PaymentCommand(OrderInfo order)
        {
            Order = order;
        }
    }
}