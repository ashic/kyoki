using System;

namespace Till.Core
{
    public class OrderViewEntry
    {
        public string OrderId { get; private set; }
        public string Email { get; private set; }
        public DateTime OrderTime { get; private set; }

        public OrderViewEntry(string orderId, string email, DateTime orderTime)
        {
            OrderId = orderId;
            Email = email;
            OrderTime = orderTime;
        }
    }
}