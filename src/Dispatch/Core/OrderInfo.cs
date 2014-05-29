using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dispatch.Core
{
    public class OrderInfo
    {
        public string OrderId { get { return _jObject["orderId"].Value<string>(); } }

        public string Email { get { return _jObject["customerDetails"]["email"].Value<string>(); } }
        public string Address { get { return _jObject["paymentDetails"]["address"].Value<string>(); }}

        private readonly JObject _jObject;

        public OrderInfo(string json)
        {
            _jObject = JObject.Parse(json);
        }

        public OrderInfo Dispatched(DateTime dispatchTime)
        {
            var jo = _jObject.DeepClone();
            jo["dispatchDetails"] = new JObject();
            jo["dispatchDetails"]["dispatchTime"] = dispatchTime;
            jo["dispatchDetails"]["shippingAddress"] = Address;

            return new OrderInfo(jo.ToString(Formatting.Indented));
        }
        public override string ToString()
        {
            return _jObject.ToString(Formatting.Indented);
        }
    }
}