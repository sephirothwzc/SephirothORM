using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
  * CLR 版本：       4.0.30319.33440
  * 类 名 称：       MSSQLSqlHelper
  * 机器名称：       SEPHIROTHBF0C
  * 命名空间：       Sephiroth_DAO.realize
  * 文 件 名：       MSSQLSqlHelper
  * 创建时间：       2015/09/23 22:09:55
  * 作    者：       吴占超
  * 说    明：
  * 修改时间：
  * 修 改 人：
 *************************************************************************************/

namespace Sephiroth_DAO.realize
{
    /// <summary>
    /// MESSQL脚本帮助类
    /// </summary>
    public class MSSQLSqlHelper : Sephiroth_IDao.ISqlHelper
    {

        #region ISqlHelper 成员

        public string Sql_Insert(Sephiroth_IDao.BaseEntity model)
        {
            StringBuilder sql = new StringBuilder();
            //获取表名
            var tablename = model.GetTableName();
            return sql.ToString();
        }

        public string Sql_Update(Sephiroth_IDao.BaseEntity model)
        {
            throw new NotImplementedException();
        }

        public string Sql_Delete(Sephiroth_IDao.BaseEntity model)
        {
            throw new NotImplementedException();
        }

        public string Sql_Select(Sephiroth_IDao.BaseEntity model, string columns = "", string wheres = "")
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
