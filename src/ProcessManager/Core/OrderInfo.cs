using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProcessManager.Core
{
    public class OrderInfo
    {
        public string OrderId { get { return _jObject["orderId"].Value<string>(); } }

        private readonly JObject _jObject;

        public OrderInfo(string json)
        {
            _jObject = JObject.Parse(json);
        }

        public override string ToString()
        {
            return _jObject.ToString(Formatting.Indented);
        }
    }
}