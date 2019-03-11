using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace IDAL
{
    public interface IBaseDAL<T>
    {
        int Insert(T userInfo);
        int Insert(IDbConnection database, IDbTransaction tran, T userInfo);
        int Delete(string id);
        int Delete(IDbConnection database, IDbTransaction tran, string id);
        int Update(string id, T userInfo);
        int Update(IDbConnection database, IDbTransaction tran, string id, T userInfo);
        IList<T> GetAll();
        T GetByID(string id);
        IList<T> GetByWhere(string sqlwhere, string orderby);
    }
}
