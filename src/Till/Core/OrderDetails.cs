using System;

namespace Till.Core
{
    public class OrderDetails
    {
        public Guid OrderId { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public decimal Price { get; private set; }
        public string Currency { get; private set; }
        public string Item { get; private set; }
        public DateTime OrderTime { get; private set; }

        public OrderDetails(Guid orderId, string email, string address, decimal price, string currency, string item, DateTime orderTime)
        {
            OrderId = orderId;
            Email = email;
            Address = address;
            Price = price;
            Currency = currency;
            Item = item;
            OrderTime = orderTime;
        }
    }
}