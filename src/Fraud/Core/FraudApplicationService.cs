using System;
using Common.Logging;

namespace Fraud.Core
{
    public class FraudApplicationService
    {
        private readonly FraudStorage _storage;
        private readonly Action<OrderPassedFraudCheckEvent> _onPaid;
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        public FraudApplicationService(FraudStorage storage, Action<OrderPassedFraudCheckEvent> onPaid)
        {
            _storage = storage;
            _onPaid = onPaid;
        }

        public void Handle(CheckOrderForFraudCommand command)
        {
            Log.InfoFormat("Processing fraud check for Order {0}", command.Order.OrderId);
            var paymentReference = Guid.NewGuid().ToString();
            var provider = "DataCash";
            var paid = command.Order.PassedFraud(20);
            _storage.OrderPassedFraudCheck(paid, 20);
            var e = new OrderPassedFraudCheckEvent(paid);
            _onPaid(e);
        }
    }
}