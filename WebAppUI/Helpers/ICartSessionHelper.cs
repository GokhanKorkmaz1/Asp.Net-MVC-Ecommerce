using Entities.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppUI.Helpers
{
    public interface ICartSessionHelper
    {
        Cart GetCart(String key);
        void SetCart(String key, Cart cart);
        void Clear();
    }
}
