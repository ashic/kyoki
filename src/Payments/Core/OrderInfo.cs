using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Payments.Core
{
    public class OrderInfo
    {
        public string OrderId { get { return _jObject["orderId"].Value<string>(); } }
        public decimal Amount { get { return _jObject["paymentDetails"]["price"].Value<decimal>(); } }
        public string Currency { get { return _jObject["paymentDetails"]["currency"].Value<string>(); } }
        public string Address { get { return _jObject["paymentDetails"]["address"].Value<string>(); }}

        private readonly JObject _jObject;

        public OrderInfo(string json)
        {
            _jObject = JObject.Parse(json);
        }

        public OrderInfo Paid(string paymentReference, string provider)
        {
            var jo = _jObject.DeepClone();
            jo["paymentDetails"]["paymentReference"] = paymentReference;
            jo["paymentDetails"]["provider"] = provider;

            return new OrderInfo(jo.ToString(Formatting.Indented));
        }

        public override string ToString()
        {
            return _jObject.ToString(Formatting.Indented);
        }
    }
}