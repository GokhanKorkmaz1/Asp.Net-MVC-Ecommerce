using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppUI.Helpers
{
    public interface ICustomerSessionHelper
    {
        Customer GetCustomer(string key);
        void SetCustomer(string key, Customer customer);
        void Clear();
    }
}
