using CodeTools.Interfaces;
using Sephiroth_IDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*************************************************************************************
    * CLR 版本：       4.0.30319.33440
    * 类 名 称：       DB_DAO
    * 机器名称：       SEPHIROTHBF0C
    * 命名空间：       DapperTools.DAO
    * 文 件 名：       DB_DAO
    * 创建时间：       15/4/29 16:51:05
    * 作    者：       吴占超
    * 说    明：       目前是mssql版本
    * 修改时间：
    * 修 改 人：
*************************************************************************************/

namespace CodeTools.DAO
{
    public class MSSQLDB_DAO<T> : Sephiroth_DAO.BaseDAO<T>, IDB_DAO where T : BaseEntity, new()
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sdao"></param>
        public MSSQLDB_DAO(DB_Connection sdao)
        {
            this.absorm = new Sephiroth_DAO.DapperDAO(sdao);//根据配置选择生成不同方式的dao实例 比如dapper、entityframework
        }

        #region 实现IDAO absORM
        /// <summary>
        /// 用于实现的IDAO 
        /// </summary>
        private IDAO absorm = null;

        public override IDAO absORM
        {
            get
            {
                return absorm;
            }
            set
            {
                absorm = value;
            }
        }
        #endregion 

        #region GetDBTree : 获取数据库当前对象
        /// <summary>
        /// 获取数据库当前对象
        /// </summary>
        /// <returns></returns>
        public IEnumerable<dynamic> GetDBTree()
        {
            string sql = @"select O.name as 表名,[value] as 说明,[object_id],PTB.name as Pname,O.type as 类型  from 
                            sys.objects O
                            left join sys.extended_properties PTB
                            on PTB.class=1
                            AND PTB.minor_id=0 
                            and O.[object_id] = PTB.major_id
                            and PTB.name = 'MS_Description'
                            where O.type='U' or O.type = 'V'
							order by 表名	
                            ";
            var rows = this.QueryDynamic(sql);
            return rows;
        }
        #endregion

        #region GetTableObject ： 获取表对象的实例
        /// <summary>
        /// 获取表对象的实例
        /// </summary>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public IEnumerable<Entity.TableObject> GetTableObject(string tablename)
        {
            #region MSsql 的实现方式
            string sql = string.Format(@"
SELECT (case
         when a.colorder = 1 then
          d.name
         else
          ''
       end) as TableName, --如果表名相同就返回空              
       a.name as ColName,
       a.colorder as colorder,
       (case
         when COLUMNPROPERTY(a.id, a.name, 'IsIdentity') = 1 then
          1
         else
          0
       end) as Identitys,
       (case
         when (SELECT count(*)
                 FROM sysobjects --查询主键                                    
                WHERE (name in
                      (SELECT name
                          FROM sysindexes
                         WHERE (id = a.id)
                           AND (indid in
                               (SELECT indid
                                   FROM sysindexkeys
                                  WHERE (id = a.id)
                                    AND (colid in
                                        (SELECT colid
                                            FROM syscolumns
                                           WHERE (id = a.id)
                                             AND (name = a.name)))))))
                  AND (xtype = 'PK')) > 0 then
          1
         else
          0
       end) as PK, --查询主键END                   
       b.name as Dtype,
       a.length as Lengths,--占用字节数
       COLUMNPROPERTY(a.id, a.name, 'PRECISION') as Precisions,--长度
       isnull(COLUMNPROPERTY(a.id, a.name, 'Scale'), 0) as Scales,--小数位数
       (case
         when a.isnullable = 1 then
          0
         else
          1
       end) as IsNullable,--允许空
       isnull(e.text, '') as Dtext,--默认值
       isnull(g.value, '') AS Remarks--字段说明
  FROM syscolumns a
  left join systypes b
    on a.xtype = b.xusertype
 inner join sysobjects d
    on a.id = d.id
   and (d.xtype = 'U' or d.xtype='V')
   and d.name <> 'dtproperties'
  left join syscomments e
    on a.cdefault = e.id
  left join sys.extended_properties g
    on a.id = g.major_id
   AND a.colid = g.minor_id
 where d.name = '{0}' --所要查询的表                                           
 order by a.id, a.colorder
", tablename);
            #endregion
            return this.absorm.Query<CodeTools.Entity.TableObject>(sql);
        }
        #endregion 
    }
}
