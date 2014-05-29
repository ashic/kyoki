using Common.Logging;
using Fraud.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Fraud.Boundary
{
    public class OrderPassedFraudPublisher
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public void Handle(OrderPassedFraudCheckEvent e)
        {
            var payload = new JObject();

            var metadata = new JObject();
            metadata["event"] = "order-passed-fraud-check";

            payload["metadata"] = metadata;
            payload["data"] = JObject.Parse(e.OrderInfo.ToString());

            var body = payload.ToString(Formatting.Indented);

            var client = new RestClient("http://localhost:8864");
            var request = new RestRequest("/", Method.POST);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            client.ExecuteAsync(request, (_, __) => { });

            Log.Info(payload.ToString(Formatting.Indented));
        }
    }
}