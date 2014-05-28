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

            payload["event"] = "order-placed";
            payload["orderId"] = e.Order.OrderId;
            payload["paymentDetails"] = new JObject();
            payload["paymentDetails"]["price"] = e.Order.Price;
            payload["paymentDetails"]["currency"] = e.Order.Currency;
            payload["paymentDetails"]["address"] = e.Order.Address;
            payload["customerDetails"] = new JObject();
            payload["customerDetails"]["email"] = e.Order.Email;
            payload["customerDetails"]["address"] = e.Order.Address;
            payload["itemDetails"] = new JObject();
            payload["itemDetails"]["item"] = e.Order.Item;
            payload["itemDetails"]["price"] = e.Order.Price;

            var body = payload.ToString(Formatting.Indented);

            var client = new RestClient("http://localhost:8051");
            var request = new RestRequest("/order-payment", Method.POST);
            request.AddBody(body);
            client.Execute(request);

            Log.Info(payload.ToString(Formatting.Indented));
        }
    }
}