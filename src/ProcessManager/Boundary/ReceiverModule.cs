using System;
using System.IO;
using Common.Logging;
using Nancy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProcessManager.Core;

namespace ProcessManager.Boundary
{
    public class ReceiverModule : NancyModule
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        public ReceiverModule(OrderProcessor processor)
        {
            Get["/"] = _ => "I'm Up!!";

            Post["/"] = _ =>
            {
                using (var sr = new StreamReader(Request.Body))
                {
                    var content = sr.ReadToEnd();

                    var jo = JObject.Parse(content);

                    Log.Info("Got a request");
                    var order = new OrderInfo(jo["data"].ToString(Formatting.Indented));

                    var eventType = jo["metadata"]["event"].Value<string>();
                    processor.Handle(eventType, order);
                }

                return Response.AsText("Accepted")
                    .WithStatusCode(HttpStatusCode.Accepted);
            };
        }
    }
}