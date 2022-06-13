using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public void AddOrder(Order order)
        {
            _orderDal.Add(order);
        }

        public void Delete(Order order)
        {
            _orderDal.Delete(order);
        }

        public List<Order> GetAll()
        {
            return _orderDal.GetList();
        }

        public List<Order> GetByCustomer(int customerId)
        {
            return _orderDal.GetList(p => p.CustomerId == customerId);
        }

        public List<Order> GetByCustomerActiveOrder(int customerId)
        {
            return _orderDal.GetList(p => p.CustomerId == customerId && p.State != "Paid");
        }

        public Order GetById(int Id)
        {
            return _orderDal.Get(p => p.Id == Id);
        }

        public void Update(Order order)
        {
            _orderDal.Update(order);
        }
    }
}
