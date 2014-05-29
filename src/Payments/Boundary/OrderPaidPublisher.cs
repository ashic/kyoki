using Common.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Payments.Core;
using RestSharp;

namespace Payments.Boundary
{
    public class OrderPaidPublisher
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public void Handle(OrderPaidEvent e)
        {
            var payload = new JObject();

            var metadata = new JObject();
            metadata["event"] = "order-paid";

            payload["metadata"] = metadata;
            payload["data"] = JObject.Parse(e.OrderInfo.ToString());

            var body = payload.ToString(Formatting.Indented);

            var client = new RestClient("http://localhost:8862");
            var request = new RestRequest("fraud-check/", Method.POST);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            client.ExecuteAsync(request, (_, __) => { });

            Log.Info(payload.ToString(Formatting.Indented));
        }
    }
}