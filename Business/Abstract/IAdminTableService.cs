using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAdminTableService
    {
        List<AdminTable> GetAll();
        List<AdminTable> GetByUsername(string username);
        AdminTable GetByInfos(string username, string password); 
    }
}
