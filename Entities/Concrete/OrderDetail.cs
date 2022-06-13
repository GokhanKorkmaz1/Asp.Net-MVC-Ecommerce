using Core.Entities.Abstract;
using Entities.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class OrderDetail:IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
    }
}
