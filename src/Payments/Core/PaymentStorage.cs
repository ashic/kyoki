using System.Collections.Generic;

namespace Payments.Core
{
    public class PaymentStorage
    {
        readonly List<PaymentsViewModel> _entries = new List<PaymentsViewModel>(); 

        public PaymentsViewModel[] GetAll()
        {
            return _entries.ToArray();
        }

        public void OrderPaid(OrderInfo request, string paymentReference, string provider)
        {
            _entries.Add(new PaymentsViewModel(request.OrderId, request.Amount, request.Currency, "Paid", paymentReference, provider));
        }
    }
}