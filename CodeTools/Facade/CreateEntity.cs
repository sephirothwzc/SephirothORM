using CodeTools.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CodeTools.Facade;
using System.Diagnostics;
using Commonly;


/*************************************************************************************
    * CLR 版本：       4.0.30319.33440
    * 类 名 称：       CreateEntity
    * 机器名称：       SEPHIROTHBF0C
    * 命名空间：       DapperTools
    * 文 件 名：       CreateEntity
    * 创建时间：       15/4/30 23:25:31
    * 作    者：       吴占超
    * 说    明：       
    * 修改时间：
    * 修 改 人：
*************************************************************************************/

namespace CodeTools
{
    /// <summary>
    /// 生成Entity对象代码.CS
    /// </summary>
    public class CreateEntity
    {
        /// <summary>
        /// 属性集合
        /// </summary>
        private IEnumerable<TableObject> tableobj { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        private string fpatch { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        private string filename { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        private string fullname { get; set; }

        /// <summary>
        /// 对象说明
        /// </summary>
        private string remark { get; set; }

        /// <summary>
        /// 是否首字母大写
        /// </summary>
        private bool toupper { get; set; }

        /// <summary>
        /// 有参数构造函数 一个文件 生成一个新对象
        /// </summary>
        /// <param name="to"></param>
        /// <param name="patch"></param>
        /// <param name="filename"></param>
        /// <param name="fullname">命名空间</param>
        public CreateEntity(IEnumerable<TableObject> to, string patch, string filename,string fullname="Entity",string remark="",bool toupper=false)
        {
            this.tableobj = to;
            this.fpatch = patch;
            this.filename = filename;
            this.fullname = fullname;
            this.remark = remark;
            this.toupper = toupper;
        }

        /// <summary>
        /// 获取数据类型
        /// </summary>
        /// <returns></returns>
        private Hashtable InitxType()
        {
            Hashtable ht = new Hashtable();
            ht.Add("image", "byte[]");
            ht.Add("text", "string");
            ht.Add("uniqueidentifier", "string");
            ht.Add("tinyint", "int");
            ht.Add("smallint", "int");
            ht.Add("int", "int");
            ht.Add("smalldatetime", "DateTime");
            ht.Add("real", "double");
            ht.Add("money", "decimal");
            ht.Add("datetime", "DateTime");
            ht.Add("float", "float");
            ht.Add("ntext", "string");
            ht.Add("bit", "bool");
            ht.Add("decimal", "decimal");
            ht.Add("varchar", "string");
            ht.Add("nvarchar", "string");
            ht.Add("char", "string");
            ht.Add("nchar", "string");
            ht.Add("numeric", "decimal");
            ht.Add("timestamp", "TimeSpan");
            
            return ht;
        }


        /// <summary>
        /// 执行文件生成返回成功失败
        /// </summary>
        public void CreateEntityRUN()
        {
            if (tableobj.Count() <= 0)
            {
                MessageBox.Show(string.Format(@"{0}属性为空请确认！", filename));
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format(@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interface_Dapper;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*************************************************************************************
    * CLR 版本：       4.0.30319.33440
    * 类 名 称：       {0}
    * 机器名称：       {1}
    * 命名空间：       {3}
    * 文 件 名：       {0}
    * 创建时间：       {2}
    * 作    者：       
    * 说    明：       
    * 修改时间：
    * 修 改 人：
*************************************************************************************/

namespace {3}
{{
    /// <summary>
    /// {4}
    /// </summary>
    [Table(""{0}"")]
    public class {0}: BaseEntity
    {{
        
", this.filename, PC_Helper.GetMachineName(),DateTime.Now.ToString(),this.fullname,this.remark));
            Hashtable ht  = this.InitxType();
            foreach (TableObject t in this.tableobj)
            {
                #region 输出字符串常量对应数据库字段属性 和属性名称相同 但是有可能和数据库大小写不同 暂且不考虑 mysql的 数据库 区分大小写
                sb.AppendLine(string.Format(@"        public const string {0} = ""{1}""; ", t.ColName.ToUpper(), this.toupper?System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(t.ColName):t.ColName));
                #endregion 

                string coldbtype = ht[t.Dtype].ToString();
                #region 设置类型可空
                if (this.IsNumber(coldbtype) &&
                    !t.IsNullable)
                {
                    coldbtype += "?";
                }
                #endregion
                sb.AppendLine(string.Format(@"        "));
                sb.AppendLine(string.Format(@"        /// <summary>"));
                sb.AppendLine(string.Format(@"        /// {0}",t.Remarks));
                sb.AppendLine(string.Format(@"        /// </summary>"));
                //sb.AppendLine(string.Format(@"        [DapperPropertyAttribute(TableName = ""{0}"",ColumName=""{1}"",PrimaryKey={2},NotNull={3},Len=""{4}"")]",
                //    this.filename,//表名
                //    t.ColName,//列名
                //    t.PK ? "true" : "false",//主键
                //    t.IsNullable ? "true" : "false",//非空
                //    this.GetLen(t,ht)));
                sb.AppendLine(string.Format(@"        [Column(""{0}"")]",t.ColName));
                //sb.AppendLine(string.Format(@"        [MaxLength({0})]", this.GetLen(t, ht)));
                sb.AppendLine(string.Format(@"        [MaxLength({0})]", t.Precisions));
                if (t.PK)
                    sb.AppendLine(@"        [Key]");
                if(t.IsNullable)
                    sb.AppendLine(@"        [Required]");
                if(!string.IsNullOrEmpty(t.Remarks))
                    sb.AppendLine(string.Format(@"        [DisplayName(""{0}"")]",t.Remarks));
                if (this.toupper)//是否首字母大写
                    sb.AppendLine(string.Format(@"        public {0} {1} {{ get; set; }}", coldbtype, System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(t.ColName)));
                else
                    sb.AppendLine(string.Format(@"        public {0} {1} {{ get; set; }}
", coldbtype, t.ColName));
            }
            sb.Append(string.Format(@"    }}
}}
"));
            string spatch = string.Format("{0}\\{1}.cs", this.fpatch,this.filename);
            using (System.IO.StreamWriter sw = new StreamWriter(spatch, false, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(sb.ToString());
            }

        }

        /// <summary>
        /// 判断是否数字类型或者bool
        /// </summary>
        /// <param name="dbtype"></param>
        /// <returns></returns>
        public bool IsNumber(string coldbtype)
        {
            return (coldbtype == "int" ||
                    coldbtype == "DateTime" ||
                    coldbtype == "double" ||
                    coldbtype == "decimal" ||
                    coldbtype == "float" ||
                    coldbtype == "bool");
        }

        /// <summary>
        /// 返回长度
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public string GetLen(TableObject t, Hashtable ht)
        {
            if (ht[t.Dtype].ToString() == "string"||
                ht[t.Dtype].ToString() == "DateTime")
                return t.Lengths;
            if (ht[t.Dtype].ToString() == "int"||
                ht[t.Dtype].ToString() == "double"||
                ht[t.Dtype].ToString() == "float"||
                ht[t.Dtype].ToString() == "decimal")
                return t.Precisions;
            //if (ht[t.Dtype].ToString() == "decimal")
            //    return string.Format("{0},{1}",t.Precisions,t.Scales);

            return t.Lengths;
        }
    }
}
