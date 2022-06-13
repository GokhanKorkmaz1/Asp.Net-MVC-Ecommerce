using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppUI.Extensions;

namespace WebAppUI.Helpers
{
    public class CustomerSessionHelper : ICustomerSessionHelper
    {
        private IHttpContextAccessor _httpContextAccessor;
        public CustomerSessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public void Clear()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
        }

        public Customer GetCustomer(string key)
        {
            Customer customerToCheck = _httpContextAccessor.HttpContext.Session.GetObject<Customer>(key);
            if (customerToCheck == null)
            {
                return null;
            }
            return customerToCheck;
        }

        public void SetCustomer(string key, Customer customer)
        {
            _httpContextAccessor.HttpContext.Session.SetObject(key, customer);
        }
    }
}
