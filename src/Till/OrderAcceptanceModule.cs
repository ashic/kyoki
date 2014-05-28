using System;
using System.Runtime.InteropServices;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;
using Till.Core;

namespace Till
{
    public class OrderAcceptanceModule : NancyModule
    {
        public OrderAcceptanceModule(TillStorage storage, OrderService svc)
        {
            Get["/"] = _ =>
            {
                var model = storage.GetAll();
                var orderForm = new OrderForm {OrderId = Guid.NewGuid()};
                ViewBag.FormData = orderForm;

                return Negotiate.WithModel(model)
                    .WithView("Views/index.cshtml");
            };

            Post["/"] = _ =>
            {
                var o = this.Bind<OrderForm>();
                var orderDetails = new OrderDetails(new Guid(o.OrderId.ToString()), o.Email, o.Address, o.Price, o.Currency, o.Item,
                    DateTime.UtcNow);
                var command = new PlaceOrderCommand(orderDetails);
                svc.Handle(command);

                return Response.AsRedirect("/");
            };
        }
    }
}