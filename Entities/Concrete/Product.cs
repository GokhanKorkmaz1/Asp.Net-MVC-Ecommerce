using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Producer { get; set; }
        public string Model { get; set; }
        public int CategoryId { get; set; }
        public int UnitinStock { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }
    }
}
