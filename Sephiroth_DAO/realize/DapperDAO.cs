using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
  * CLR 版本：       4.0.30319.33440
  * 类 名 称：       DapperDAO
  * 机器名称：       SEPHIROTHBF0C
  * 命名空间：       Sephiroth_DAO.realize
  * 文 件 名：       DapperDAO
  * 创建时间：       2015/09/23 16:02:07
  * 作    者：       吴占超
  * 说    明：
  * 修改时间：
  * 修 改 人：
 *************************************************************************************/

namespace Sephiroth_DAO
{
    /// <summary>
    /// dapper 实现 IDAO
    /// </summary>
    public class DapperDAO : Sephiroth_IDao.IDAO 
    {
        #region 私有属性
        /// <summary>
        /// 对象数据库处理类
        /// </summary>
        private Sephiroth_IDao.DB_Connection dbconn { get; set; }

        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        private string connection { get; set; }
        #endregion

        #region 构造函数

        #endregion 

        #region IDAO 成员

        string Sephiroth_IDao.IDAO.GetSqlConnection()
        {
            throw new NotImplementedException();
        }

        void Sephiroth_IDao.IDAO.SetSqlConnection(Sephiroth_IDao.DB_Connection db_Connection)
        {
            throw new NotImplementedException();
        }

        IEnumerable<T> Sephiroth_IDao.IDAO.Query<T>(T param, IEnumerable<string> columns, string paramwhere)
        {
            throw new NotImplementedException();
        }

        IEnumerable<dynamic> Sephiroth_IDao.IDAO.QueryDynamic(string sql, object param)
        {
            throw new NotImplementedException();
        }

        IEnumerable<T> Sephiroth_IDao.IDAO.Query<T>(string sql, object param)
        {
            throw new NotImplementedException();
        }

        int Sephiroth_IDao.IDAO.Insert<T>(T param)
        {
            throw new NotImplementedException();
        }

        int Sephiroth_IDao.IDAO.Insert<T>(IEnumerable<T> param, bool transaction)
        {
            throw new NotImplementedException();
        }

        int Sephiroth_IDao.IDAO.Update<T>(T param)
        {
            throw new NotImplementedException();
        }

        int Sephiroth_IDao.IDAO.Update<T>(IEnumerable<T> param, bool all)
        {
            throw new NotImplementedException();
        }

        int Sephiroth_IDao.IDAO.Delete<T>(T param)
        {
            throw new NotImplementedException();
        }

        int Sephiroth_IDao.IDAO.Delete<T>(IEnumerable<T> param)
        {
            throw new NotImplementedException();
        }

        int Sephiroth_IDao.IDAO.Execute<T>(string sql, T param)
        {
            throw new NotImplementedException();
        }

        int Sephiroth_IDao.IDAO.Execute<T>(string sql, IEnumerable<T> param)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
