using System.Collections.Generic;
using System.Linq;

namespace Till.Core
{
    public class TillStorage
    {
        readonly List<OrderViewEntry> _orders = new List<OrderViewEntry>(); 
        public void Add(OrderViewEntry order)
        {
            if (_orders.Any(x => x.OrderId == order.OrderId))
                return;

            _orders.Add(order);
        }

        public List<OrderViewEntry> GetAll()
        {
            return _orders;
        }
    }
}