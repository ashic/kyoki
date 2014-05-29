using System;
using System.Collections.Generic;
using Common.Logging;
using ProcessManager.Boundary;

namespace ProcessManager.Core
{
    public class OrderProcessor
    {
        private readonly EventPublisher _publisher;
        readonly Dictionary<string, OrderStatus> _state = new Dictionary<string, OrderStatus>();
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public OrderProcessor(EventPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(string messageType, OrderInfo order)
        {
            if (messageType == "order-placed")
            {
                _state[order.OrderId] = OrderStatus.Placed;
                _publisher.PayOrder(order);
            }
            else
            {
                var state = _state[order.OrderId];

                switch (state)
                {
                    case OrderStatus.Placed:
                        if (messageType == "order-paid")
                        {
                            _state[order.OrderId] = OrderStatus.Paid;
                            _publisher.FraudCheckOrder(order);
                        }
                        break;
                    case OrderStatus.Paid:
                        if (messageType == "order-passed-fraud-check")
                        {
                            _state[order.OrderId] = OrderStatus.FraudPassed;
                            _publisher.DispatchOrder(order);
                        }
                        break;
                    case OrderStatus.FraudPassed:
                        if (messageType == "order-dispatched")
                        {
                            _state.Remove(order.OrderId);
                           Log.InfoFormat("Processing complete for order {0}", order.OrderId);
                        }
                        break;
                }
            }
        }
    }
}