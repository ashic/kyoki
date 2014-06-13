using Common.Logging;
using Dispatch.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Dispatch.Boundary
{
    public class OrderDispatchedPublisher
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public void Handle(OrderDispatchedEvent e)
        {
            var payload = new JObject();

            payload["data"] = JObject.Parse(e.OrderInfo.ToString());

            sendToNextStep(payload);

            Log.Info(payload.ToString(Formatting.Indented));
        }

        private static void sendToNextStep(JObject payload)
        {
            var todo = (JArray)payload["data"]["routingSlip"]["todo"];

            var first = todo.First;

            var done = (JArray)payload["data"]["routingSlip"]["done"];
            done.Add(first);

            todo.Remove(first);

            if (todo.Count == 0)
            {
                Log.InfoFormat("Order processing complete.");
                return;
            }

            var next = todo.First.Value<string>();
            var body = payload.ToString(Formatting.Indented);

            var client = new RestClient(next);
            var request = new RestRequest("/", Method.POST);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            client.ExecuteAsync(request, (_, __) => { });
        }
    }
}