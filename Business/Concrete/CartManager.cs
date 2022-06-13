using Business.Abstract;
using Entities.Concrete;
using Entities.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CartManager : ICartService
    {
        public void AddToCart(Cart cart, Product product)
        {
            CartLine cartline = cart.CartLines.FirstOrDefault(c => c.Product.Id == product.Id);
            if(cartline != null)
            {
                cartline.Quantity++;
                return;
            }
            else
            {
                cart.CartLines.Add(new CartLine { Product = product, Quantity = 1 });
            }
        }

        public List<CartLine> List(Cart cart)
        {
            return cart.CartLines;
        }

        public void RemoveFromCart(Cart cart, int productId)
        {
            CartLine cartLine = cart.CartLines.FirstOrDefault(c => c.Product.Id == productId);
            if (cartLine.Quantity > 1)
            {
                cartLine.Quantity--;
                return;
            }
            else
            {
                cart.CartLines.Remove(cart.CartLines.FirstOrDefault(c => c.Product.Id == productId));
            }
        }

        public void IncreaseProductQuantity(Cart cart,int productId)
        {
            CartLine cartLine = cart.CartLines.FirstOrDefault(c => c.Product.Id == productId);
            cartLine.Quantity++;
        }
    }
}
