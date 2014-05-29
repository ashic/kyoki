using Dispatch.Core;
using Nancy;

namespace Dispatch
{
    public class DispatchModule : NancyModule
    {
        public DispatchModule(DispatchStorage storage)
        {
            Get["/"] = _ =>
            {
                var model = storage.GetAll();

                return Negotiate.WithModel(model)
                    .WithView("Views/index.cshtml");
            };
        }
    }
}