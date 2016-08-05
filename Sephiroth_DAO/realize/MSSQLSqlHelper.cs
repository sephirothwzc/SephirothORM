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

namespace Sephiroth_DAO
{
    /// <summary>
    /// MESSQL脚本帮助类
    /// </summary>
    public class MSSQLSqlHelper : Sephiroth_IDao.ISqlHelper
    {
        #region ISqlHelper 成员
        
        /// <summary>
        /// 根据对象生成insert语句返回
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Sql_Insert(Sephiroth_IDao.BaseEntity model)
        {
            StringBuilder sql = new StringBuilder();
            //生成主键
            model.SetInsertKey();
            sql.AppendFormat("insert into {0} ", model.GetTableName());//表名

            List<string> cols = new List<string>();
            List<string> vals = new List<string>();

            //映射字段 并且 属性值不为 空
            var properties = model.GetType().GetProperties().Where(p => 
                !model.PropertyNotMapped(p) 
                && p.GetValue(model) != null 
                && p.GetValue(model) != DBNull.Value).ToList();

            properties.ForEach(pa =>
            {
                ColumnAttribute col = model.PropertyColumn(pa);
                cols.Add(col == null ? pa.Name : col.Name);//有列名特性用特性没特性用属性名
                vals.Add(string.Format("@{0}", pa.Name));//根据属性名生成参数
            });
            if (cols.Count == 0)
                throw new Exception("colums count is zero");//输出异常 没有列符合数据
            sql.AppendFormat("({0}) values ({1}) ;", string.Join(",", cols), string.Join(",", vals));
            return sql.ToString();
        }

        /// <summary>
        /// 根据model生成update语句返回
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Sql_Update(Sephiroth_IDao.BaseEntity model)
        {
            //获取属性
            var pArray = model.GetType().GetProperties().ToList();
            //判断特性当前表字段生成 表字段不等于当前对象特性 tablename 则认为不是当前字段
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("update {0} set ", model.GetTableName());

            List<string> strset = new List<string>();
            List<string> strkey = new List<string>();
            //映射字段 并且 属性值不为 空
            pArray.Where(p => !model.PropertyNotMapped(p) 
                && p.GetValue(model) != null 
                && p.GetValue(model) != DBNull.Value).ToList().ForEach(pa =>
            {
                ColumnAttribute col = model.PropertyColumn(pa);
                string temp = string.Format(" {0} = @{1} ", col == null ? pa.Name : col.Name, pa.Name);
                if (model.PropertyKey(pa))
                    strkey.Add(temp);
                else
                    strset.Add(temp);
            });
            if (strset.Count == 0)
                throw new Exception("colums count is zero");
            if (strkey.Count == 0)
                throw new Exception("pk count is zero");

            sql.AppendFormat(" {0} where {1}", string.Join(" , ", strset), string.Join(" and ", strkey));
            return sql.ToString();
        }

        /// <summary>
        /// 根据对象生成delete语句返回
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Sql_Delete(Sephiroth_IDao.BaseEntity model)
        {
            StringBuilder sql = new StringBuilder();
            List<string> strkey = new List<string>();
            //映射字段 并且 属性值不为 空
            model.GetType().GetProperties().Where(p => 
                !model.PropertyNotMapped(p) 
                && p.GetValue(model) != null 
                && p.GetValue(model) != DBNull.Value).ToList()
                .ForEach(pa =>
            {
                ColumnAttribute col = model.PropertyColumn(pa);
                string temp = string.Format(" {0} = @{1} ", col == null ? pa.Name : col.Name, pa.Name);
                strkey.Add(temp);
            });

            if (strkey.Count == 0)
                throw new Exception("pk count is zero");
            sql.AppendFormat("delete {0} where {1} ", model.GetTableName(), string.Join(" and ", strkey));
            return sql.ToString();
        }

        /// <summary>
        /// 根据对象生成select
        /// </summary>
        /// <param name="model">对象</param>
        /// <param name="columns">获取列</param>
        /// <param name="wheres">自定义条件</param>
        /// <returns></returns>
        public string Sql_Select(Sephiroth_IDao.BaseEntity model, IEnumerable<string> columns = null, string wheres = "")
        {
            StringBuilder sql = new StringBuilder();
            List<string> strcolumn = new List<string>();
            if (columns != null && columns.Count() > 0)
                strcolumn.AddRange(columns);//自定义条件组成
            List<string> strwhere = new List<string>();
            strwhere.Add(" 1=1 ");//占位条件

            if (wheres != "")
                strwhere.Add(wheres);
            if (columns == null || wheres == "")//如果条件和获取列都已经指定则不需要根据属性进行遍历
            {
                model.GetType().GetProperties().Where(p => !model.PropertyNotMapped(p))
                    .ToList()
                    .ForEach(pa =>
                {
                    ColumnAttribute ca = model.PropertyColumn(pa);
                    if (columns == null)//自动生成列名
                    {
                        if (ca == null)
                            strcolumn.Add(pa.Name);
                        else
                            strcolumn.Add(string.Format("{0} {1}", ca.Name,
                                ca.Name == pa.Name ? "" : " as " + pa.Name));
                    }
                    if (wheres == "")//自动生成条件
                    {
                        if (pa.GetValue(model, null) != null && pa.GetValue(model, null) != DBNull.Value)//属性值不为空
                        {
                            string temp = string.Format(" {0} = @{1} ", ca == null ? pa.Name : ca.Name, pa.Name);
                            strwhere.Add(temp);
                        }
                    }
                });
            }

            if (strcolumn.Count == 0)
                throw new Exception("columns count is zero");

            sql.AppendFormat(" select {0} from {1} where {2} ;", string.Join(",", strcolumn), model.GetTableName(), string.Join(" and ", strwhere));
            return sql.ToString();
        }

        #endregion
    }
}
