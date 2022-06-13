using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class AdminTable : IEntity
    {

        public string Username { get; set; }
        [Key]
        public string Password { get; set; }
    }
}
