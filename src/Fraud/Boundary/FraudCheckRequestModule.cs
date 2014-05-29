using System.IO;
using Common.Logging;
using Fraud.Core;
using Nancy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fraud.Boundary
{
    public class FraudCheckRequestModule : NancyModule
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        public FraudCheckRequestModule(FraudApplicationService svc)
        {
            Post["/fraud-check"] = x =>
            {
                using (var sr = new StreamReader(Request.Body))
                {
                    var content = sr.ReadToEnd();

                    var jo = JObject.Parse(content);

                    if (jo["metadata"]["event"].Value<string>() == "order-paid")
                    {
                        Log.Info("Got a request");
                        var order = new OrderInfo(jo["data"].ToString(Formatting.Indented));
                        var command = new CheckOrderForFraudCommand(order);
                        svc.Handle(command);
                    }
                }

                return HttpStatusCode.Accepted;
            };
        }
    }

    
}