using Dispatch.Boundary;
using Dispatch.Core;
using Nancy;
using Nancy.TinyIoc;

namespace Dispatch
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<DispatchStorage>().AsSingleton();

            var publisher = new OrderPassedFraudPublisher();
            var svc = new DispatchApplicationService(container.Resolve<DispatchStorage>(), publisher.Handle);
            container.Register(svc);
        }
    }
}