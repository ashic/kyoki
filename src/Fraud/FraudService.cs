using System;
using Nancy.Hosting.Self;

namespace Fraud
{
    public class FraudService
    {
        private NancyHost _nancy;

        public void Start()
        {
            var config = new HostConfiguration
            {
                RewriteLocalhost = false
            };

            _nancy = new NancyHost(config, new Uri("http://localhost:8862/"));
            _nancy.Start();

        }

        public void Stop()
        {
            _nancy.Dispose();
        }
    }
}