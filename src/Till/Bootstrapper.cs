using System;
using Nancy;
using Nancy.TinyIoc;
using Till.Boundary;
using Till.Core;

namespace Till
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<TillStorage>().AsSingleton();

            Action<OrderPlacedEvent> onOrderPlaced = new OrderAcceptedHttpPublisher().Handle;

            var svc = new OrderService(container.Resolve<TillStorage>(), onOrderPlaced);
            container.Register(svc);
        }
    }
}