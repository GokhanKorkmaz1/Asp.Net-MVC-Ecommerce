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
    public class CustomerController : Controller
    {
        private ICustomerService _customerService;
        private ICustomerSessionHelper _customerSessionHelper;

        public CustomerController(ICustomerService customerService, ICustomerSessionHelper customerSessionHelper)
        {
            _customerService = customerService;
            _customerSessionHelper = customerSessionHelper;
        }
        public IActionResult Index()
        {
            var model = new CustomerListViewModel
            {
                Customers = _customerService.GetAll()
            };
            return View(model);
        }

        public IActionResult SignIn()
        {
            var model = new CustomerAddViewModel()
            {
                customer = new Customer()
            };
            return View(model);
        }

        public IActionResult CreateAnAccount()
        {
            var model = new CustomerAddViewModel
            {
                customer = new Customer()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateAnAccount(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            TempData.Add("message", "Account created successfully!");
            _customerService.Add(customer);
            _customerSessionHelper.SetCustomer("customer", customer);
            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public IActionResult SignIn(Customer customer)
        {
            customer = _customerService.GetByInfos(customer.PhoneNumber, customer.Password);
            if (customer != null)
            {
                _customerSessionHelper.SetCustomer("customer", customer);
                var msg = "Sign in as " + customer.Name.ToString();
                TempData.Add("message", msg);
                return RedirectToAction("Index", "Product");
            }
            else
            {
                TempData.Add("message", "Please check your informations");
                return RedirectToAction("SignIn", "Customer");
            }

        }

        public IActionResult SignOut()
        {
            _customerSessionHelper.Clear();
            TempData.Add("message", "Sign out");
            return RedirectToAction("Index", "Product");
        }

        public IActionResult AccountSettings()
        {
            var model = new CustomerAddViewModel
            {
                customer = _customerSessionHelper.GetCustomer("customer")
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult AccountSettings(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var sessionCustomer = _customerSessionHelper.GetCustomer("customer");
            customer.Id = sessionCustomer.Id;
            customer.Password = sessionCustomer.Password;
            _customerSessionHelper.Clear();
            _customerSessionHelper.SetCustomer("customer", customer);
            _customerService.Update(customer);
            TempData.Add("message", "Your information has been successfully updated");
            return RedirectToAction("Index", "Product");
        }

        public IActionResult ChangePassword()
        {
            var model = new CustomerAddViewModel
            {
                customer = _customerSessionHelper.GetCustomer("customer")
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ChangePassword(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var newPassword = customer.Password;
            customer = _customerSessionHelper.GetCustomer("customer");
            customer.Password = newPassword;
            _customerSessionHelper.Clear();
            _customerSessionHelper.SetCustomer("customer", customer);
            _customerService.Update(customer);
            TempData.Add("message", "Your password has been successfully changed");
            return RedirectToAction("Index", "Product");
        }
    }
}
