using Nancy;
using Payments.Core;

namespace Payments
{
    public class PaymentsModule : NancyModule
    {
        public PaymentsModule(PaymentStorage storage)
        {
            Get["/"] = _ =>
            {
                var model = storage.GetAll();

                return Negotiate.WithModel(model)
                    .WithView("Views/index.cshtml");
            };
        }
    }
}