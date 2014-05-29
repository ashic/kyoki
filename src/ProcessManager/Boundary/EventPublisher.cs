using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProcessManager.Core;
using RestSharp;

namespace ProcessManager.Boundary
{
    public class EventPublisher
    {
        public void PayOrder(OrderInfo order)
        {
            var data = JObject.Parse(order.ToString());
            var metadata = new JObject();
            metadata["command"] = "pay-order";

            var payload = new JObject();
            payload["metadata"] = metadata;
            payload["data"] = data;

            var body = payload.ToString(Formatting.Indented);
            var client = new RestClient("http://localhost:8861");
            var request = new RestRequest("order-payment/", Method.POST);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            client.ExecuteAsync(request, (_, __) => { });

            //Log.Info(payload.ToString(Formatting.Indented));
        }

        public void FraudCheckOrder(OrderInfo order)
        {
            var data = JObject.Parse(order.ToString());
            var metadata = new JObject();
            metadata["command"] = "fraud-check-order";

            var payload = new JObject();
            payload["metadata"] = metadata;
            payload["data"] = data;

            var body = payload.ToString(Formatting.Indented);
            var client = new RestClient("http://localhost:8862");
            var request = new RestRequest("fraud-check/", Method.POST);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            client.ExecuteAsync(request, (_, __) => { });
        }

        public void DispatchOrder(OrderInfo order)
        {
            var data = JObject.Parse(order.ToString());
            var metadata = new JObject();
            metadata["command"] = "dispatch-order";

            var payload = new JObject();
            payload["metadata"] = metadata;
            payload["data"] = data;

            var body = payload.ToString(Formatting.Indented);
            var client = new RestClient("http://localhost:8863");
            var request = new RestRequest("dispatch-order/", Method.POST);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            client.ExecuteAsync(request, (_, __) => { });
        }
    }
}