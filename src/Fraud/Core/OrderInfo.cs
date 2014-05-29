using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fraud.Core
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

        public OrderInfo PassedFraud(int score)
        {
            var jo = _jObject.DeepClone();
            jo["fraudDetails"] = new JObject();
            jo["fraudDetails"]["passed"] = true;
            jo["fraudDetails"]["score"] = score;

            return new OrderInfo(jo.ToString(Formatting.Indented));
        }

        public override string ToString()
        {
            return _jObject.ToString(Formatting.Indented);
        }
    }
}