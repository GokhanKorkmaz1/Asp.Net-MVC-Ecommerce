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
    public class OrderController : Controller
    {
        private ICartService _cartService;
        private ICartSessionHelper _cartSessionHelper;
        private IProductService _productService;
        private IOrderService _orderService;
        private IOrderSessionHelper _orderSessionHelper;
        private ICustomerSessionHelper _customerSessionHelper;
        private IOrderDetailService _orderDetailService;
        public OrderController(ICartService cartService, ICartSessionHelper cartSessionHelper, IProductService productService, IOrderService orderService, IOrderDetailService orderDetailService, IOrderSessionHelper orderSessionHelper, ICustomerSessionHelper customerSessionHelper)
        {
            _cartService = cartService;
            _cartSessionHelper = cartSessionHelper;
            _productService = productService;
            _orderService = orderService;
            _orderSessionHelper = orderSessionHelper;
            _customerSessionHelper = customerSessionHelper;
            _orderDetailService = orderDetailService;
        }

        public IActionResult ShowByCustomerId()
        {
            var customerId = _customerSessionHelper.GetCustomer("customer").Id;
            var model = new OrderListViewModel
            {
                orders = _orderService.GetByCustomer(customerId),
            };
            return View(model);
        }

        public IActionResult Complete()
        {
            if (_customerSessionHelper.GetCustomer("customer") == null)
            {
                TempData.Add("message", "Please Sign In");
                return RedirectToAction("SignIn", "Customer");
            }
            _orderSessionHelper.SetOrderList("orders", _orderService.GetByCustomer(_customerSessionHelper.GetCustomer("customer").Id));
            var model = new OrderViewModel
            {
                order = new Order()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Complete(Order order)
        {
            if (string.IsNullOrWhiteSpace(order.ShippingAddress) || order.Installment==0)
            {
                return View();
            }
            order.CustomerId = _customerSessionHelper.GetCustomer("customer").Id;
            order.DateofPurchase = DateTime.Now;
            order.RemainingInstallment = order.Installment;
            order.State = "waiting for approval";
            _orderService.AddOrder(order);

            var cart = _cartSessionHelper.GetCart("cart");
            foreach (var cartline in cart.CartLines)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = cartline.Product.Id,
                    Price = cartline.Product.Price,
                    Quantity = cartline.Quantity,
                    TotalPrice = cartline.Product.Price * cartline.Quantity
                };
                var product = _productService.GetById(cartline.Product.Id);

                if (cartline.Quantity > product.UnitinStock)
                {
                    TempData.Add("message", "There are not enough " + cartline.Product.Producer + " " + cartline.Product.Model + ". We have " + product.UnitinStock + " in stock.");
                    _orderService.Delete(order);
                    return RedirectToAction("Index", "Cart");
                }
                product.UnitinStock -= cartline.Quantity;
                _productService.Update(product);
                _orderDetailService.Add(orderDetail);
            }
            TempData.Add("message", "Your order has been successfully completed.");
            return RedirectToAction("Index", "Product");
        }
        public IActionResult OnlinePayment()
        {
            var model = new OrderListViewModel
            {
                orders = _orderService.GetByCustomerActiveOrder(_customerSessionHelper.GetCustomer("customer").Id)
            };
            return View(model);
        }

        public IActionResult Payment(int orderId)
        {
            var order = _orderService.GetById(orderId);
            var total = 0.0;
            if(order.State == "Approved")
            {
                foreach (var item in _orderDetailService.GetByOrderId(order.Id))
                {
                    total += item.TotalPrice;
                }
                order.RemainingInstallment--;
                if (order.RemainingInstallment == 0)
                {
                    order.State = "Paid";
                }
                TempData.Add("message", "The payment has been made successfully");
                _orderService.Update(order);
                return RedirectToAction("OnlinePayment", "Order");
            }
            else
            {
                TempData.Add("message", "Your order must be confirmed in order to make a payment");
                return RedirectToAction("OnlinePayment", "Order");
            }
        }

        public IActionResult ShowOrderDetail(int orderId)
        {
            var model = new OrderDetailViewModel
            {
                order = _orderDetailService.GetByOrderId(orderId)
            };
            return View(model);
        }

        public IActionResult Return(int orderId)
        {
            var order = _orderService.GetById(orderId);
            var orderDetail = _orderDetailService.GetByOrderId(orderId);
            foreach (var item in orderDetail)
            {
                var product = _productService.GetById(item.ProductId);
                product.UnitinStock += item.Quantity;
                _productService.Update(product);
            }
            _orderService.Delete(order);
            TempData.Add("message", "Your order has been successfully canceled");
            return RedirectToAction("Index", "Product");
        }
    }
}
