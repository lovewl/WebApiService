using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace SqlServerDAL
{
    [Serializable]
    public class UserDAL : IUserDAL
    {
        public int Delete(string id)
        {
            throw new NotImplementedException();
        }

        public int Delete(IDbConnection database, IDbTransaction tran, string id)
        {
            throw new NotImplementedException();
        }

        public IList<UserInfo> GetAll()
        {
            List<UserInfo> userInfos = new List<UserInfo>();
            var dt = SqlServerHelper.Execute("SELECT * FROM T_ORDER");
            foreach (DataRow dr in dt.Rows)
            {
                userInfos.Add(new UserInfo() { ID = int.Parse(dr["ID"].ToString()), Name = dr["ORDER_TYPE"].ToString(), LoginName = dr["FILM_NAME"].ToString() });
            }
            return userInfos;
        }

        public UserInfo GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public IList<UserInfo> GetByWhere(string sqlwhere, string orderby)
        {
            throw new NotImplementedException();
        }

        public int Insert(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }

        public int Insert(IDbConnection database, IDbTransaction tran, UserInfo userInfo)
        {
            throw new NotImplementedException();
        }

        public int Update(string id, UserInfo userInfo)
        {
            throw new NotImplementedException();
        }

        public int Update(IDbConnection database, IDbTransaction tran, string id, UserInfo userInfo)
        {
            throw new NotImplementedException();
        }
    }
}
