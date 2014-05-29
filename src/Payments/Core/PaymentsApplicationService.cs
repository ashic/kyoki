using System;
using Common.Logging;

namespace Payments.Core
{
    public class PaymentsApplicationService
    {
        private readonly PaymentStorage _storage;
        private readonly Action<OrderPaidEvent> _onPaid;
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        public PaymentsApplicationService(PaymentStorage storage, Action<OrderPaidEvent> onPaid)
        {
            _storage = storage;
            _onPaid = onPaid;
        }

        public void Handle(PaymentCommand command)
        {
            Log.InfoFormat("Processing payment for Order {0}", command.Order.OrderId);
            var paymentReference = Guid.NewGuid().ToString();
            var provider = "DataCash";
            var paid = command.Order.Paid(paymentReference, provider);
            _storage.OrderPaid(paid, paymentReference, provider);
            var e = new OrderPaidEvent(paid);
            _onPaid(e);
        }
    }
}