using Sephiroth_IDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
  * CLR 版本：       4.0.30319.33440
  * 类 名 称：       BaseDAO
  * 机器名称：       SEPHIROTHBF0C
  * 命名空间：       Sephiroth_DAO
  * 文 件 名：       BaseDAO
  * 创建时间：       2015/09/23 14:40:23
  * 作    者：       吴占超
  * 说    明：
  * 修改时间：
  * 修 改 人：
 *************************************************************************************/

namespace Sephiroth_DAO
{
    /// <summary>
    /// 超级DAO 实现了Sephiroth_IDao.IDAO EntityDAO继承
    /// </summary>
    public abstract class BaseDAO<T>   where T : BaseEntity, new()
    {
        /// <summary>
        /// 必须实现absORM
        /// </summary>
        public abstract Sephiroth_IDao.IDAO absORM { get; set; }

        #region IDAO 成员

        public string GetSqlConnection()
        {
            return absORM.GetSqlConnection();
        }

        public void SetSqlConnection(Sephiroth_IDao.DB_Connection db_Connection)
        {
            absORM.SetSqlConnection(db_Connection);
        }

        public IEnumerable<T> Query(T param, IEnumerable<string> columns = null, string paramwhere = "") 
        {
            return absORM.Query(param, columns, paramwhere);
        }

        public IEnumerable<dynamic> QueryDynamic(string sql, object param = null)
        {
            return absORM.QueryDynamic(sql, param);
        }

        /// <summary>
        /// 用于匿名类型参数查询返回BaseEntity
        /// </summary>
        /// <param name="sql">sql 脚本</param>
        /// <param name="param">匿名类型参数</param>
        /// <returns></returns>
        public IEnumerable<T> Query(string sql, object param = null) 
        {
            return absORM.Query<T>(sql, param);
        }

        public int Insert(T param) 
        {
            return absORM.Insert(param);
        }

        public int Insert(IEnumerable<T> param, bool transaction = true) 
        {
            return absORM.Insert(param, transaction);
        }

        public int Update(T param)
        {
            return absORM.Update(param);
        }

        public int Update(IEnumerable<T> param, bool all = false) 
        {
            return absORM.Update(param, all);
        }

        public int Delete(T param) 
        {
            return absORM.Delete(param);
        }

        public int Delete(IEnumerable<T> param) 
        {
            return absORM.Delete(param);
        }

        public int Execute(string sql, T param) 
        {
            return absORM.Execute(sql, param);
        }

        public int Execute(string sql, object param)
        {
            return absORM.Execute(sql, param);
        }

        public int Execute(string sql, IEnumerable<T> param)
        {
            return absORM.Execute(sql, param);
        }

        public int Execute(string sql, IEnumerable<object> param)
        {
            return absORM.Execute(sql, param);
        }
        #endregion
    }
}
