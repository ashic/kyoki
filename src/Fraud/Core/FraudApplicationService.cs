using System;
using Common.Logging;

namespace Fraud.Core
{
    public class FraudApplicationService
    {
        private readonly FraudStorage _storage;
        private readonly Action<OrderPassedFraudCheckEvent> _onFraudPassed;
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        public FraudApplicationService(FraudStorage storage, Action<OrderPassedFraudCheckEvent> onFraudPassed)
        {
            _storage = storage;
            _onFraudPassed = onFraudPassed;
        }

        public void Handle(CheckOrderForFraudCommand command)
        {
            Log.InfoFormat("Processing fraud check for Order {0}", command.Order.OrderId);
            var paymentReference = Guid.NewGuid().ToString();
            var provider = "DataCash";
            var done = command.Order.PassedFraud(20);
            _storage.OrderPassedFraudCheck(done, 20);
            var e = new OrderPassedFraudCheckEvent(done);
            _onFraudPassed(e);
        }
    }
}