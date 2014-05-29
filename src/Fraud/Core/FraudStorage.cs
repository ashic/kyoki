using System.Collections.Generic;

namespace Fraud.Core
{
    public class FraudStorage
    {
        readonly List<FraudCheckResultViewModel> _entries = new List<FraudCheckResultViewModel>(); 

        public FraudCheckResultViewModel[] GetAll()
        {
            return _entries.ToArray();
        }

        public void OrderPassedFraudCheck(OrderInfo request, int score)
        {
            _entries.Add(new FraudCheckResultViewModel(request.OrderId, request.Email, score, "Passed"));
        }
    }
}