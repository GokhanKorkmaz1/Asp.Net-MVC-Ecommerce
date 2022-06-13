using Core.Entities.Abstract;
using Entities.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string State { get; set; }
        public int Installment { get; set; }
        public int RemainingInstallment { get; set; }
        public DateTime DateofPurchase { get; set; }
        public string ShippingAddress { get; set; }

    }
}
