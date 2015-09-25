using Sephiroth_IDao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SephirothCommon;

/*************************************************************************************
  * CLR 版本：       4.0.30319.33440
  * 类 名 称：       DapperDAO
  * 机器名称：       SEPHIROTHBF0C
  * 命名空间：       Sephiroth_DAO.realize
  * 文 件 名：       DapperDAO
  * 创建时间：       2015/09/23 16:02:07
  * 作    者：       吴占超
  * 说    明：       BO层指定实现IDAO的方式，实现多种orm方式进行实现 每种dao实现方式根据DB_Connection生成不同的数据库兼容
  * 修改时间：
  * 修 改 人：
 *************************************************************************************/

namespace Sephiroth_DAO
{
    /// <summary>
    /// dapper 实现 IDAO 用于porxy创建
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

        /// <summary>
        /// 脚本生成对象 根据数据库类型动态创建
        /// </summary>
        private ISqlHelper sqlhelper { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        public DapperDAO(Sephiroth_IDao.DB_Connection dbconn)
        {
            this.dbconn = dbconn;
            this.SetSqlConnection(this.dbconn);
            this.sqlhelper = ReflectionHelper.CreateInstance<ISqlHelper>(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace,
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Namespace,
                this.dbconn.dbtype + "_Helper");
        }
        #endregion 
    
    
        #region IDAO 成员
        /// <summary>
        /// 获取当前链接字符串
        /// </summary>
        /// <returns></returns>
        public string GetSqlConnection()
        {
            return this.connection;
        }

        /// <summary>
        /// 设置数据库链接字符串
        /// </summary>
        /// <param name="db_Connection"></param>
        public void SetSqlConnection(Sephiroth_IDao.DB_Connection db_Connection)
        {
            switch (db_Connection.dbtype)
            {
                case Sephiroth_IDao.DB_Connection.e_DBType.MSSQL:
                    connection = string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};"
                        , db_Connection.dbsource
                        , db_Connection.datasource
                        , db_Connection.username
                        , db_Connection.password);
                    break;
                case Sephiroth_IDao.DB_Connection.e_DBType.MYSQL:
                    connection = string.Format(@"server={0};database={1};uid={2};pwd={3};charset='gbk'"
                        , db_Connection.dbsource
                        , db_Connection.datasource
                        , db_Connection.username
                        , db_Connection.password);
                    break;
                case Sephiroth_IDao.DB_Connection.e_DBType.ORACLE:
                    connection = string.Format(@"data source = {0};user id = {1};password = {2}"
                        , db_Connection.datasource
                        , db_Connection.username
                        , db_Connection.password);
                    break;
            }
        }

        /// <summary>
        /// 获取查询结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <param name="columns"></param>
        /// <param name="paramwhere"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(T param, IEnumerable<string> columns = null, string paramwhere = "") where T : new()
        {
            ISqlHelper sqlhelper = this.GetSqlHelper(this.sysDao.dbtype);
            using (IDbConnection conn = OpenConnection())
            {
                string sql;
                if (paramwhere == "")// 有条件生成sql
                    sql = sqlhelper.Sql_Select(param == null ? new T() : param, columns, paramwhere);
                else//有对象无条件无列名，全自动生成  and 条件的 所有列名 sql
                    sql = sqlhelper.Sql_Select(param == null ? new T() : param);
                return conn.Query<T>(sql, param);
            }
            return null;
        }

        public IEnumerable<dynamic> QueryDynamic(string sql, object param = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query<T>(string sql, object param = null) where T : new()
        {
            throw new NotImplementedException();
        }

        public int Insert<T>(T param) where T : new()
        {
            throw new NotImplementedException();
        }

        public int Insert<T>(IEnumerable<T> param, bool transaction = true) where T : new()
        {
            throw new NotImplementedException();
        }

        public int Update<T>(T param) where T : new()
        {
            throw new NotImplementedException();
        }

        public int Update<T>(IEnumerable<T> param, bool all = false) where T : new()
        {
            throw new NotImplementedException();
        }

        public int Delete<T>(T param) where T : new()
        {
            throw new NotImplementedException();
        }

        public int Delete<T>(IEnumerable<T> param) where T : new()
        {
            throw new NotImplementedException();
        }

        public int Execute<T>(string sql, T param) where T : new()
        {
            throw new NotImplementedException();
        }

        public int Execute(string sql, object param)
        {
            throw new NotImplementedException();
        }

        public int Execute<T>(string sql, IEnumerable<T> param)
        {
            throw new NotImplementedException();
        }

        public int Execute(string sql, IEnumerable<object> param)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
