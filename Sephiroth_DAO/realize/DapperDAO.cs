using Sephiroth_IDao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SephirothCommon;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Reflection;

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
                this.dbconn.dbtype + "SqlHelper");//根据数据库类型
        }
        #endregion 

        #region OpenConnection：返回数据库链接
        public IDbConnection OpenConnection(IDbTransaction idbtransaction = null)
        {
            if (idbtransaction != null)//如果事务对象创建则不需要重新生成
                return idbtransaction.Connection;

            IDbConnection sqlconn;
            switch (this.dbconn.dbtype)
            {
                case DB_Connection.e_DBType.MSSQL:
                    sqlconn = new SqlConnection(this.connection);
                    sqlconn.Open();
                    return sqlconn;
                case DB_Connection.e_DBType.MYSQL:
                    sqlconn = new MySqlConnection(this.connection);
                    sqlconn.Open();
                    return sqlconn;
                //case DB_Connection.e_DBType.ORACLE:
                //    sqlconn = new OracleConnection(this.connection);
                //    sqlconn.Open();
                //    return sqlconn;
            }
            return null;
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
            switch (dbconn.dbtype)
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

        #region 事务创建
        public IDbTransaction CreateIDbTransaction()
        {
            return OpenConnection().BeginTransaction();
        }
        #endregion 

        /// <summary>
        /// 获取查询结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="param"></param>
        /// <param name="columns"></param>
        /// <param name="paramwhere"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(T param = null, IEnumerable<string> columns = null, string paramwhere = "", IDbTransaction idbtransaction = null) where T : BaseEntity, new()
        {
            IDbConnection conn = OpenConnection(idbtransaction);

            string sql;
            if (paramwhere == "")// 有条件生成sql
                sql = sqlhelper.Sql_Select(param == null ? new T() : param, columns, paramwhere);
            else//有对象无条件无列名，全自动生成  and 条件的 所有列名 sql
                sql = sqlhelper.Sql_Select(param == null ? new T() : param);
#if DEBUG
            Debug.Print(sql);
#endif
            return conn.Query<T>(sql, param, idbtransaction);
        }

        public IEnumerable<dynamic> QueryDynamic(string sql, object param = null, IDbTransaction idbtransaction = null)
        {
            IDbConnection conn = OpenConnection(idbtransaction);
#if DEBUG
            Debug.Print(sql);
#endif
            return conn.Query(sql, param, idbtransaction);
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction idbtransaction = null) where T : BaseEntity, new()
        {
            IDbConnection conn = OpenConnection(idbtransaction);
#if DEBUG
            Debug.Print(sql);
#endif
            return conn.Query<T>(sql, param, idbtransaction);

        }

        public int Insert<T>(T param, IDbTransaction idbtransaction = null) where T : BaseEntity, new()
        {
            try
            {
                string sql = sqlhelper.Sql_Insert(param);
                int row = 0;
                IDbConnection conn = OpenConnection(idbtransaction);
                row = conn.Execute(sql, param, idbtransaction);
                //根据业务发生情况决定是否 追加 数据库主键自增的查询
                return row;
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.Print(ex.Message);
#endif
                throw ex;
            }
        }

        public int Insert<T>(IEnumerable<T> param, bool firstsql = true, IDbTransaction idbtransaction = null) where T : BaseEntity, new()
        {
            try
            {
                if (firstsql)
                {
                    string sql = sqlhelper.Sql_Insert(param.FirstOrDefault());
                    return this.Execute(sql, param, idbtransaction);
                }
                else
                {
                    int i = 1;
                    foreach (T item in param)
                    {
                        i += this.Insert(item, idbtransaction);
                    }
                    return i;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Debug.Print(ex.Message);
#endif
                throw ex;
            }
        }

        public int Update<T>(T param, IDbTransaction idbtransaction = null) where T : BaseEntity, new()
        {
            string sql = sqlhelper.Sql_Update(param);
            return this.Execute(sql, param, idbtransaction);
        }

        public int Update<T>(IEnumerable<T> param, bool all = false, IDbTransaction idbtransaction = null) where T : BaseEntity, new()
        {
            if (all)
            {
                string sql = sqlhelper.Sql_Update(param.FirstOrDefault());
                return this.Execute(sql, param, idbtransaction);
            }
            else
            {
                int i = 1;
                foreach (T item in param)
                {
                    i += this.Update(item, idbtransaction);
                }
                return i;
            }
        }

        public int Delete<T>(T param, IDbTransaction idbtransaction = null) where T : BaseEntity, new()
        {
            string sql = sqlhelper.Sql_Delete(param);
            return this.Execute(sql, param, idbtransaction);
        }

        public int Delete<T>(IEnumerable<T> param, IDbTransaction idbtransaction = null) where T : BaseEntity, new()
        {
            string sql = sqlhelper.Sql_Delete(param.FirstOrDefault());
            return this.Execute(sql, param, idbtransaction);
        }

        public int Execute<T>(string sql, T param, IDbTransaction idbtransaction = null) where T : BaseEntity, new()
        {
            IDbConnection conn = OpenConnection(idbtransaction);
#if DEBUG
            Debug.Print(sql);
#endif
            return conn.Execute(sql, param, idbtransaction);
        }

        public int Execute(string sql, object param, IDbTransaction idbtransaction = null)
        {
            IDbConnection conn = OpenConnection(idbtransaction);
#if DEBUG
            Debug.Print(sql);
#endif
            return conn.Execute(sql, param, idbtransaction);
        }

        public int Execute<T>(string sql, IEnumerable<T> param, IDbTransaction idbtransaction = null)
        {
            IDbConnection conn = OpenConnection(idbtransaction);
#if DEBUG
            Debug.Print(sql);
#endif
            return conn.Execute(sql, param, idbtransaction);
        }

        public int Execute(string sql, IEnumerable<object> param, IDbTransaction idbtransaction = null)
        {
            IDbConnection conn = OpenConnection(idbtransaction);
#if DEBUG
            Debug.Print(sql);
#endif
            return conn.Execute(sql, param, idbtransaction);
        }

        #endregion 

        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">首对象</typeparam>
        /// <typeparam name="TSecond">join对象</typeparam>
        /// <typeparam name="TRetun">返回对象</typeparam>
        /// <param name="sql">脚本</param>
        /// <param name="map">赋值处理方法</param>
        /// <param name="splitOn">分割对象列名 数据库字段名</param>
        /// <param name="param"></param>
        /// <param name="idbtransaction"></param>
        /// <returns></returns>
        public IEnumerable<TRetun> Query<T, TSecond, TRetun>(string sql, Func<T, TSecond, TRetun> map, string splitOn, object param = null, IDbTransaction idbtransaction = null) where T : BaseEntity, new()
        {
            IDbConnection conn = OpenConnection(idbtransaction);
#if DEBUG
            Debug.Print(sql);
#endif
            return conn.Query<T, TSecond, TRetun>(sql, map, param, idbtransaction, splitOn: splitOn);
        }
    }
}
