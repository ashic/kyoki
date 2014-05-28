using System;

namespace Till.Core
{
    public class OrderService
    {
        private readonly TillStorage _storage;
        private readonly Action<OrderPlacedEvent> _onOrderPlaced;

        public OrderService(TillStorage storage, Action<OrderPlacedEvent> onOrderPlaced)
        {
            _storage = storage;
            _onOrderPlaced = onOrderPlaced;
        }

        public void Handle(PlaceOrderCommand command)
        {
            var order = command.Order;
            _storage.Add(new OrderViewEntry(order.OrderId.ToString(), order.Email, order.OrderTime));
            _onOrderPlaced(new OrderPlacedEvent(order));            
        }
    }
}