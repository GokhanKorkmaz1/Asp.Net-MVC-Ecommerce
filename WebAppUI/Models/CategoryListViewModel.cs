using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppUI.Models
{
    public class CategoryListViewModel 
    {
        public List<Category> Categories { get; set; }
        public int CurrentCategory { get; set; }
    }
}
