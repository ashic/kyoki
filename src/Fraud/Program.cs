using Topshelf;
using Topshelf.Common.Logging;

namespace Fraud
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<FraudService>(s =>
                {
                    s.ConstructUsing(() => new FraudService());
                    s.WhenStarted(_ => _.Start());
                    s.WhenStopped(_ => _.Stop());
                });

                x.SetDescription("Fraud");
                x.SetDisplayName("Fraud");
                x.SetServiceName("Fraud");
                x.UseCommonLogging();
            });
        }
    }
}
