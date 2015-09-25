using CodeTools.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*************************************************************************************
    * CLR 版本：       4.0.30319.33440
    * 类 名 称：       IDB_DAO
    * 机器名称：       SEPHIROTHBF0C
    * 命名空间：       DapperTools.Interfaces
    * 文 件 名：       IDB_DAO
    * 创建时间：       15/4/29 21:28:35
    * 作    者：       吴占超
    * 说    明：       
    * 修改时间：
    * 修 改 人：
*************************************************************************************/

namespace CodeTools.Interfaces
{
    public interface IDB_DAO
    {
        IEnumerable<dynamic> GetDBTree();

        IEnumerable<TableObject> GetTableObject(string tablename);
    }
}
