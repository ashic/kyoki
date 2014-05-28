using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Topshelf;

namespace Till
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<TillService>(
                    s =>
                    {
                        s.ConstructUsing(() => new TillService());
                        s.WhenStarted(_ => _.Start());
                        s.WhenStopped(_ => _.Stop());
                    });
                
                x.SetDescription("Till");
                x.SetDisplayName("Till");
                x.SetServiceName("Till");
            });
        }
    }

    public class TillService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        public TillService()
        {
            
        }

        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }
    }
}
