using System;

namespace Till.Core
{
    public class OrderForm
    {
        public Guid OrderId { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string Item { get; set; }
    }
}