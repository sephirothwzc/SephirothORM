using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*************************************************************************************
  * CLR 版本：       4.0.30319.33440
  * 类 名 称：       IDAO
  * 机器名称：       SEPHIROTHBF0C
  * 命名空间：       Sephiroth_IDao
  * 文 件 名：       IDAO
  * 创建时间：       2015/09/22 10:30:53
  * 作    者：       吴占超
  * 说    明：
  * 修改时间：
  * 修 改 人：
 *************************************************************************************/

namespace Sephiroth_IDao
{
    /// <summary>
    /// DAO base
    /// </summary>
    public interface IDAO
    {
        #region 数据库链接
        /// <summary>
        /// 返回当前对象的数据库链接字符串
        /// </summary>
        /// <returns></returns>
        string GetSqlConnection();

        /// <summary>
        /// 设置当前对象的数据库链接字符串
        /// </summary>
        /// <returns></returns>
        void SetSqlConnection(DB_Connection db_Connection);
        #endregion 

        #region 事务创建
        IDbTransaction CreateIDbTransaction();
        #endregion 

        #region 查询
        /// <summary>
        /// 查询返回数据集
        /// </summary>
        /// <typeparam name="T">获取对象原型</typeparam>
        /// <param name="param">查询条件参数对象</param>
        /// <param name="columns">获取列，默认返回当前对象mapping列</param>
        /// <param name="paramwhere">查询条件 例'username=@UserName'属性对应param.[UserName]</param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(T param, IEnumerable<string> columns = null, string paramwhere = "", IDbTransaction idbtransaction = null) where T : BaseEntity, new();

        /// <summary>
        /// 查询返回数据集
        /// </summary>
        /// <param name="sql">完整sql</param>
        /// <param name="param">参数用对象</param>
        /// <returns></returns>
        IEnumerable<dynamic> QueryDynamic(string sql, object param = null, IDbTransaction idbtransaction = null);



        /// <summary>
        /// 查询返回 建议返回BaseEntity
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction idbtransaction = null) where T : BaseEntity, new();
        #endregion

        #region Insert

        /// <summary>
        /// 插入对象属性为空则不生成insert 返回受影响行数
        /// </summary>
        /// <typeparam name="T">泛型对象建议BaseEntity</typeparam>
        /// <param name="param">插入对象</param>
        /// <returns>受影响行数</returns>
        int Insert<T>(T param, IDbTransaction idbtransaction = null) where T : BaseEntity, new();

        /// <summary>
        /// 插入集合是否事务执行默认true
        /// </summary>
        /// <typeparam name="T">泛型对象建议BaseEntity</typeparam>
        /// <param name="param">插入对象</param>
        /// <param name="firstsql">true以第一对象生成脚本false每个对象逐个生成脚本</param>
        /// <returns>受影响行数</returns>
        int Insert<T>(IEnumerable<T> param, bool firstsql = true, IDbTransaction idbtransaction = null) where T : BaseEntity, new();
        #endregion

        #region Update
        /// <summary>
        /// 单个对象更新for key
        /// </summary>
        /// <typeparam name="T">泛型对象建议BaseEntity</typeparam>
        /// <param name="param">更新对象</param>
        /// <returns>受影响行数</returns>
        int Update<T>(T param, IDbTransaction idbtransaction = null) where T : BaseEntity, new();

        /// <summary>
        /// 集合更新主键不允许为空
        /// </summary>
        /// <typeparam name="T">泛型对象建议BaseEntity</typeparam>
        /// <param name="param">更新对象集合</param>
        /// <param name="all">true：以第一个对象为准生成sql批量执行插入。false：逐个生成sql</param>
        /// <returns>受影响行数</returns>
        int Update<T>(IEnumerable<T> param, bool all = false, IDbTransaction idbtransaction = null) where T : BaseEntity, new();
        #endregion

        #region Delete
        /// <summary>
        /// 根据对象主键删除
        /// </summary>
        /// <typeparam name="T">泛型对象建议BaseEntity</typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        int Delete<T>(T param, IDbTransaction idbtransaction = null) where T : BaseEntity, new();

        /// <summary>
        /// 根据主键删除批量执行
        /// </summary>
        /// <typeparam name="T">泛型对象建议BaseEntity</typeparam>
        /// <param name="param"></param>
        /// <returns></returns>
        int Delete<T>(IEnumerable<T> param, IDbTransaction idbtransaction = null) where T : BaseEntity, new();
        #endregion

        #region Execute
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <typeparam name="T">泛型对象建议BaseEntity</typeparam>
        /// <param name="sql">sql脚本</param>
        /// <param name="param">参数对象</param>
        /// <returns>受影响行数</returns>
        int Execute<T>(string sql, T param, IDbTransaction idbtransaction = null) where T : BaseEntity, new();

        /// <summary>
        ///  执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        int Execute(string sql, object param, IDbTransaction idbtransaction = null);

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <typeparam name="T">泛型对象建议BaseEntity</typeparam>
        /// <param name="sql">sql脚本</param>
        /// <param name="param">参数对象</param>
        /// <returns></returns>
        int Execute<T>(string sql, IEnumerable<T> param, IDbTransaction idbtransaction = null);

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        int Execute(string sql, IEnumerable<object> param, IDbTransaction idbtransaction = null);
        #endregion
    }
}
