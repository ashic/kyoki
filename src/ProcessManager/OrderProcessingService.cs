using System;
using Nancy;
using Nancy.Hosting.Self;
using Nancy.TinyIoc;
using ProcessManager.Core;

namespace ProcessManager
{
    public class OrderProcessingService
    {
        private NancyHost _nancy;

        public void Start()
        {
            var config = new HostConfiguration
            {
                RewriteLocalhost = false
            };

            _nancy = new NancyHost(config, new Uri("http://localhost:8864/"));
            _nancy.Start();

        }

        public void Stop()
        {
            _nancy.Dispose();
        }
    }

    public class MyBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<OrderProcessor>().AsSingleton();
        }
    }
}