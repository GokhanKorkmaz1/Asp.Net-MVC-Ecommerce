using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IOrderService
    {
        List<Order> GetAll();
        List<Order> GetByCustomer(int customerId);
        List<Order> GetByCustomerActiveOrder(int customerId);
        Order GetById(int Id);
        void AddOrder(Order order);
        void Update(Order order);
        void Delete(Order order);
    }
}
