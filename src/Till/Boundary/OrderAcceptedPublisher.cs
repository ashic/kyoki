using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using Common.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Till.Core;

namespace Till.Boundary
{
    public class OrderAcceptedHttpPublisher
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public void Handle(OrderPlacedEvent e)
        {
            var payload = new JObject();

            var data = new JObject();
            data["orderId"] = e.Order.OrderId;
            data["paymentDetails"] = new JObject();
            data["paymentDetails"]["price"] = e.Order.Price;
            data["paymentDetails"]["currency"] = e.Order.Currency;
            data["paymentDetails"]["address"] = e.Order.Address;
            data["customerDetails"] = new JObject();
            data["customerDetails"]["email"] = e.Order.Email;
            data["customerDetails"]["address"] = e.Order.Address;
            data["itemDetails"] = new JObject();
            data["itemDetails"]["item"] = e.Order.Item;
            data["itemDetails"]["price"] = e.Order.Price;

            payload["data"] = data;
            payload["data"]["routingSlip"] = OrderRouter.GetRoutingSlip();

            sendToNextStep(payload);

            Log.Info(payload.ToString(Formatting.Indented));
        }

        private static void sendToNextStep(JObject payload)
        {
            var todo = (JArray)payload["data"]["routingSlip"]["todo"] ;
            
            if(todo.Count == 0)
                return;

            var next = todo.First.Value<string>();

            var body = payload.ToString(Formatting.Indented);

            var client = new RestClient(next);
            var request = new RestRequest("/", Method.POST);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            client.ExecuteAsync(request, (_, __) => { });
        }
    }

    public class OrderRouter
    {
        public static JObject GetRoutingSlip()
        {
            var jo = new JObject();
            object[] todoSteps = {"http://localhost:8861/order-payment", "http://localhost:8862/fraud-check", "http://localhost:8863/dispatch-order"};
            jo["todo"] = new JArray(todoSteps);
            jo["done"] = new JArray();
            jo["inFailure"] = false;

            return jo;
        }
    }
}