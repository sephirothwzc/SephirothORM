using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*************************************************************************************
  * CLR 版本：       4.0.30319.33440
  * 类 名 称：       ISqlHelper
  * 机器名称：       SEPHIROTHBF0C
  * 命名空间：       Sephiroth_IDao
  * 文 件 名：       ISqlHelper
  * 创建时间：       2015/09/23 22:06:29
  * 作    者：       吴占超
  * 说    明：
  * 修改时间：
  * 修 改 人：
 *************************************************************************************/

namespace Sephiroth_IDao
{
    /// <summary>
    /// ISqlHelper SQL脚本接口
    /// </summary>
    public interface ISqlHelper
    {
        /// <summary>
        /// 新增sql创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string Sql_Insert(BaseEntity model);

        /// <summary>
        /// 修改sql创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string Sql_Update(BaseEntity model);

        /// <summary>
        /// 删除sql创建
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string Sql_Delete(BaseEntity model);

        /// <summary>
        /// 查询sql创建
        /// </summary>
        /// <param name="model"></param>
        /// <param name="columns"></param>
        /// <param name="wheres"></param>
        /// <returns></returns>
        string Sql_Select(BaseEntity model, string columns = "", string wheres = "");
    }
}
