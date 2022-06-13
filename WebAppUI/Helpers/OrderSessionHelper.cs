using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppUI.Extensions;

namespace WebAppUI.Helpers
{
    public class OrderSessionHelper : IOrderSessionHelper
    {
        private IHttpContextAccessor _httpContextAccessor;

        public OrderSessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Clear()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
        }

        public Order GetOrder(string key)
        {
            Order orderToCheck = _httpContextAccessor.HttpContext.Session.GetObject<Order>(key);
            if (orderToCheck == null)
            {
                return null;
            }
            return orderToCheck;
        }

        public List<Order> GetOrderList(string key)
        {
            List<Order> ordersToCheck = _httpContextAccessor.HttpContext.Session.GetObject<List<Order>>(key);
            if (ordersToCheck == null)
                return null;
            return ordersToCheck;
        }

        public void SetOrder(string key, Order order)
        {
            _httpContextAccessor.HttpContext.Session.SetObject(key, order);
        }

        public void SetOrderList(string key, List<Order> orders)
        {
            _httpContextAccessor.HttpContext.Session.SetObject(key, orders);
        }
    }
}
