using Fraud.Boundary;
using Fraud.Core;
using Nancy;
using Nancy.TinyIoc;

namespace Fraud
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<FraudStorage>().AsSingleton();

            var publisher = new OrderPassedFraudPublisher();
            var svc = new FraudApplicationService(container.Resolve<FraudStorage>(), publisher.Handle);
            container.Register(svc);
        }
    }
}