using System;
using Nancy.Hosting.Self;

namespace Payments
{
    public class PaymentsService
    {
        private NancyHost _nancy;

        public void Start()
        {
            var config = new HostConfiguration
            {
                RewriteLocalhost = false
            };

            _nancy = new NancyHost(config, new Uri("http://localhost:8861/"));
            _nancy.Start();

        }

        public void Stop()
        {
            
        }
    }
}