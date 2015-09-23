using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
  * CLR 版本：       4.0.30319.33440
  * 类 名 称：       DB_Connection
  * 机器名称：       SEPHIROTHBF0C
  * 命名空间：       Sephiroth_IDao
  * 文 件 名：       DB_Connection
  * 创建时间：       2015/09/22 22:41:15
  * 作    者：       吴占超
  * 说    明：
  * 修改时间：
  * 修 改 人：
 *************************************************************************************/

namespace Sephiroth_IDao
{
    /// <summary>
    /// 数据链接
    /// </summary>
    public class DB_Connection
    {
        /// <summary>
        /// 数据源 ip||服务
        /// </summary>
        public string dbsource { get; set; }

        /// <summary>
        /// 数据库
        /// </summary>
        public string datasource { get; set; }

        /// <summary>
        /// 数据库类型 MSSQL ORACLE MYSQL
        /// </summary>
        public e_DBType dbtype { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 数据库类型枚举
        /// </summary>
        public enum e_DBType
        {
            MSSQL,
            ORACLE,
            MYSQL
        }
    }
}
