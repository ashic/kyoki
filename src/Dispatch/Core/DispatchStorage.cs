using System;
using System.Collections.Generic;

namespace Dispatch.Core
{
    public class DispatchStorage
    {
        readonly List<DispatchViewModel> _entries = new List<DispatchViewModel>(); 

        public DispatchViewModel[] GetAll()
        {
            return _entries.ToArray();
        }

        public void OrderDispatched(OrderInfo info, DateTime dispatchTime)
        {
            _entries.Add(new DispatchViewModel(info.OrderId, info.Email, dispatchTime, info.Address));
        }
    }
}