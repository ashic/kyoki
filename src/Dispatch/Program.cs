using Topshelf;
using Topshelf.Common.Logging;

namespace Dispatch
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<DispatchService>(s =>
                {
                    s.ConstructUsing(() => new DispatchService());
                    s.WhenStarted(_ => _.Start());
                    s.WhenStopped(_ => _.Stop());
                });

                x.SetDescription("Dispatch");
                x.SetDisplayName("Dispatch");
                x.SetServiceName("Dispatch");
                x.UseCommonLogging();
            });
        }
    }
}
