using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

/*************************************************************************************
  * CLR 版本：       4.0.30319.33440
  * 类 名 称：       BaseEntity
  * 机器名称：       SEPHIROTHBF0C
  * 命名空间：       Sephiroth_IDao
  * 文 件 名：       BaseEntity
  * 创建时间：       2015/09/22 10:30:53
  * 作    者：       吴占超
  * 说    明：
  * 修改时间：
  * 修 改 人：
 *************************************************************************************/

namespace Sephiroth_IDao
{
    /// <summary>
    /// VO base
    /// </summary>
    [Serializable]
    public class BaseEntity
    {
        /// <summary>
        /// 获取当前 TableName
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            var tn = this.GetType().GetCustomAttributes<TableAttribute>(false).FirstOrDefault();
            return tn == null ? this.GetType().Name : tn.Name;
        }
    }
}
