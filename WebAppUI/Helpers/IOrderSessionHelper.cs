using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppUI.Helpers
{
    public interface IOrderSessionHelper
    {
        List<Order> GetOrderList(String key);
        void SetOrderList(String key, List<Order> orders);
        Order GetOrder(String key);
        void SetOrder(String key, Order order);
        void Clear();
    }
}
