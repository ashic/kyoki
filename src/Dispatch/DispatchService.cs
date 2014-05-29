using System;
using Nancy.Hosting.Self;

namespace Dispatch
{
    public class DispatchService
    {
        private NancyHost _nancy;

        public void Start()
        {
            var config = new HostConfiguration
            {
                RewriteLocalhost = false
            };

            _nancy = new NancyHost(config, new Uri("http://localhost:8863/"));
            _nancy.Start();

        }

        public void Stop()
        {
            _nancy.Dispose();
        }
    }
}