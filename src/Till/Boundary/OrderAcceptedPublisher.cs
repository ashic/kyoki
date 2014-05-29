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

            var metadata = new JObject();
            metadata["event"] = "order-placed";

            payload["metadata"] = metadata;
            payload["data"] = data;

            var body = payload.ToString(Formatting.Indented);

            var client = new RestClient("http://localhost:8861");
            var request = new RestRequest("order-payment/", Method.POST);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            client.ExecuteAsync(request, (_,__) => { });

            Log.Info(payload.ToString(Formatting.Indented));
        }
    }
}