using Newtonsoft.Json.Linq;

namespace ProcessManager.Core
{
    public class OrderInfo
    {
        private JObject _jObject;

        public OrderInfo(string json)
        {
            _jObject = JObject.Parse(json);
        }
    }
}