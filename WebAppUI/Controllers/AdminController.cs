using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppUI.Models;

namespace WebAppUI.Controllers
{
    public class AdminController : Controller
    {
        private IAdminTableService _adminService;
        private IOrderService _orderService;
        private IProductService _productService;
        public AdminController(IAdminTableService adminService, IOrderService orderService, IProductService productService)
        {
            _adminService = adminService;
            _orderService = orderService;
            _productService = productService;
        }

        public IActionResult SignInAdmin()
        {
            var model = new AdminViewModel
            {
                Admin = new AdminTable()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult SignInAdmin(AdminTable admin)
        {
            admin = _adminService.GetByInfos(admin.Username, admin.Password);
            if (admin != null)
            {
                TempData.Add("message", "Admin login successful");
                return RedirectToAction("ShowOrders", "Admin");
            }
            else
            {
                TempData.Add("message", "Please check your informations");
                return RedirectToAction("SignInAdmin", "Admin");
            }
        }

        public IActionResult SignOut()
        {
            TempData.Add("message", "Sign out");
            return RedirectToAction("Index", "Product");
        }

        public IActionResult ShowOrders()
        {
            var model = new OrderListViewModel
            {
                orders = _orderService.GetAll()
            };
            return View(model);
        }

        public IActionResult AddProduct()
        {
            var model = new AddProductViewModel
            {
                product = new Product()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                _productService.Add(product);
                TempData.Add("message", "The product has been successfully added");
                return RedirectToAction("AdminProduct", "Product");
            }
        }

        public IActionResult Approve(int orderId)
        {
            var order = _orderService.GetById(orderId);
            if(order.State== "waiting for approval")
            {
                order.State = "Approved";
                _orderService.Update(order);
            }
            return RedirectToAction("ShowOrders", "Admin");
        }

        public IActionResult RemoveProduct(int productId)
        {
            var product = _productService.GetById(productId);
            _productService.Delete(product);
            return RedirectToAction("AdminProduct", "Product");
        }

        public IActionResult UpdateProduct(int productId)
        {
            if (TempData["product"] != null)
            {
                TempData.Remove("product");
            }
            TempData.Add("product", productId);
            var model = new AddProductViewModel
            {
                product = _productService.GetById(productId)
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                product.Id = (int)TempData["product"];
                TempData.Remove("product");
                _productService.Update(product);
                TempData.Add("message", "The product has been successfully updated");
                return RedirectToAction("AdminProduct", "Product");
            }
        }
    }
}
