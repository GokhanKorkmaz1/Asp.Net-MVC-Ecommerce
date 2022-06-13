using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppUI.Helpers;
using WebAppUI.Models;

namespace WebAppUI.Controllers
{
    public class CartController:Controller
    {
        private ICartService _cartService;
        private ICartSessionHelper _cartSessionHelper;
        private IProductService _productService;
        private IOrderService _orderService;

        public CartController(ICartService cartService, ICartSessionHelper cartSessionHelper, IProductService productService, IOrderService orderService)
        {
            _cartService = cartService;
            _cartSessionHelper = cartSessionHelper;
            _productService = productService;
            _orderService = orderService;
        }

        public IActionResult AddToCart(int productId)
        {
            Product product = _productService.GetById(productId);

            var cart = _cartSessionHelper.GetCart("cart");

            _cartService.AddToCart(cart, product);

            _cartSessionHelper.SetCart("cart", cart);

            TempData.Add("message", product.Producer + " " + product.Model + " added to cart!");
            
            return RedirectToAction("Index","Product");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            Product product = _productService.GetById(productId);

            var cart = _cartSessionHelper.GetCart("cart");

            _cartService.RemoveFromCart(cart, productId);

            _cartSessionHelper.SetCart("cart", cart);

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Increase(int productId)
        {
            var cart = _cartSessionHelper.GetCart("cart"); 
            _cartService.IncreaseProductQuantity(cart, productId);
            _cartSessionHelper.SetCart("cart", cart);
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Index()
        {
            var model = new CartListViewModel
            {
                Cart = _cartSessionHelper.GetCart("cart")
            };
            return View(model);
        }
    }
}
