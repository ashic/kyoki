using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Common.Logging;

namespace ProcessManager
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<OrderProcessingService>(s =>
                {
                    s.ConstructUsing(() => new OrderProcessingService());
                    s.WhenStarted(_ => _.Start());
                    s.WhenStopped(_ => _.Stop());
                });

                x.SetDescription("ProcessManager");
                x.SetDisplayName("ProcessManager");
                x.SetServiceName("ProcessManager");
                x.UseCommonLogging();
            });
        }
    }
}
