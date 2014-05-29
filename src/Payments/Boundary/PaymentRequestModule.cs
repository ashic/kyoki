using System;
using System.IO;
using Common.Logging;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Payments.Core;

namespace Payments.Boundary
{
    public class PaymentRequestModule : NancyModule
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        public PaymentRequestModule(PaymentsApplicationService svc)
        {
            Post["/order-payment"] = x =>
            {
                using (var sr = new StreamReader(Request.Body))
                {
                    var content = sr.ReadToEnd();

                    var jo = JObject.Parse(content);

                    if (jo["metadata"]["event"].Value<string>() == "order-placed")
                    {
                        Log.Info("Got a request");
                        var order = new OrderInfo(jo["data"].ToString(Formatting.Indented));
                        var command = new PaymentCommand(order);
                        svc.Handle(command);
                    }
                }

                return HttpStatusCode.Accepted;
            };
        }
    }

    
}