using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AdminTableManager : IAdminTableService
    {
        private IAdminTableDal _adminTableDal;
        public AdminTableManager(IAdminTableDal adminTableDal)
        {
            _adminTableDal = adminTableDal;
        }
        public List<AdminTable> GetAll()
        {
            return _adminTableDal.GetList();
        }

        public AdminTable GetByInfos(string username, string password)
        {
            return _adminTableDal.Get(a => a.Username == username && a.Password == password);
        }

        public List<AdminTable> GetByUsername(string username)
        {
            return _adminTableDal.GetList(p => p.Username == username);
        }
    }
}
