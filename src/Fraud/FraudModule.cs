using Fraud.Core;
using Nancy;

namespace Fraud
{
    public class FraudModule : NancyModule
    {
        public FraudModule(FraudStorage storage)
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