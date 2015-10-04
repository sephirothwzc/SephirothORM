using Sephiroth_IDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
    * CLR 版本：       4.0.30319.33440
    * 类 名 称：       CreateSYSDAO
    * 机器名称：       SEPHIROTHBF0C
    * 命名空间：       DAO
    * 文 件 名：       CreateSYSDAO
    * 创建时间：       15/5/17 09:23:45
    * 作    者：       吴占超
    * 说    明：       
    * 修改时间：
    * 修 改 人：
*************************************************************************************/

namespace Sephiroth_DAO
{
    public class CreateDB_Connection
    {
        public static DB_Connection GetSephiroth_System()
        {
            return new DB_Connection
            {
                datasource = System.Configuration.ConfigurationManager.AppSettings["datasource"],
                dbsource = System.Configuration.ConfigurationManager.AppSettings["dbsource"],
                dbtype = (DB_Connection.e_DBType)Enum.Parse(typeof(DB_Connection.e_DBType), System.Configuration.ConfigurationManager.AppSettings["dbtype"]??"MSSQL") ,
                username = System.Configuration.ConfigurationManager.AppSettings["username"],
                password = System.Configuration.ConfigurationManager.AppSettings["password"]
            };
        }
    }
}
