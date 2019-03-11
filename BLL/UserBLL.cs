using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BLL
{
    public class UserBLL
    {
        private IUserDAL userDAL = FactoryDAL.CreateDAL<IUserDAL>();
        public IList<UserInfo> GetALL()
        {
            var list = userDAL.GetAll();
            return list;
        }
    }
}
