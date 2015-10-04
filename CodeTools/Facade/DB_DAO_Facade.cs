using SephirothCommon;
using CodeTools.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeTools.Interfaces;
using Sephiroth_IDao;


/*************************************************************************************
    * CLR 版本：       4.0.30319.33440
    * 类 名 称：       DB_DAO_Facade
    * 机器名称：       SEPHIROTHBF0C
    * 命名空间：       DapperTools.Facade
    * 文 件 名：       DB_DAO_Facade
    * 创建时间：       15/4/30 09:20:55
    * 作    者：       吴占超
    * 说    明：       
    * 修改时间：
    * 修 改 人：
*************************************************************************************/

namespace CodeTools.Facade
{
    /// <summary>
    /// 处理DAO业务的外观接口封装
    /// </summary>
    public class DB_DAO_Facade
    {
        /// <summary>
        /// DB_dao实例
        /// </summary>
        private IDB_DAO idb;

        public DB_DAO_Facade(DB_Connection dao)
        {
            this.idb = ReflectionHelper.CreateInstanceT<IDB_DAO>("CodeTools", "CodeTools.DAO", Login.dao.dbtype + "DB_DAO", typeof(BaseEntity), new object[] { dao });
            //根据数据库类型不同生成不同DB_DAO 例如 [MSSQL][DB_DAO]
        }
        /// <summary>
        /// 通过反射 生成IDB_DAO接口实例
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> IDB_DAO_GetDBTree()
        {
            return from items in this.idb.GetDBTree().ToList()
                                           select new { 
                                               items.表名, 
                                               items.说明,
                                               items.Pname,
                                               items.object_id,
                                               items.类型
                                           };
        }

        /// <summary>
        /// 根据对象id获取表对象数据字典
        /// </summary>
        /// <param name="obj_id"></param>
        /// <returns></returns>
        internal IEnumerable<TableObject> IDB_DAO_GetTableObject(string tablename)
        {
            return this.idb.GetTableObject(tablename);
        }
    }
}
