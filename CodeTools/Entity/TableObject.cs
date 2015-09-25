using Sephiroth_IDao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeTools.Entity
{
    public class TableObject : BaseEntity
    {
        /// <summary>
        /// 表名
        /// </summary>
        public new string TableName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public object Dtype { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Precisions { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public bool PK { get; set; }

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNullable { get; set; }
        
        /// <summary>
        /// 长度
        /// </summary>
        public string Lengths { get; set; }
    }
}
