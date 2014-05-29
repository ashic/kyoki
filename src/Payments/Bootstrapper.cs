using Nancy;
using Nancy.TinyIoc;
using Payments.Boundary;
using Payments.Core;

namespace Payments
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<PaymentStorage>().AsSingleton();

            var publisher = new OrderPaidPublisher();
            var svc = new PaymentsApplicationService(container.Resolve<PaymentStorage>(), publisher.Handle);
            container.Register(svc);
        }
    }
}