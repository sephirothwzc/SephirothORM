using SephirothCommon;
using CodeTools.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


/*************************************************************************************
   * CLR 版本：       4.0.30319.33440
   * 类 名 称：       CreateDAO
   * 机器名称：       SEPHIROTHBF0C
   * 命名空间：       DapperTools.Facade
   * 文 件 名：       CreateDAO
   * 创建时间：       15/5/29 09:07:17
   * 作    者：       吴占超
   * 说    明：        
   * 修改时间：
   * 修 改 人：
  *************************************************************************************/

namespace CodeTools.Facade
{
    public class CreateDAO
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
        public CreateDAO(IEnumerable<TableObject> to, string patch, string filename,string fullname="DAO",string remark="",bool toupper=false)
        {
            this.tableobj = to;
            this.fpatch = patch;
            this.filename = filename;
            this.fullname = fullname;
            this.remark = remark;
            this.toupper = toupper;
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
using System.Threading.Tasks;
using Entity;
using Sephiroth_IDao;
using System.ComponentModel.DataAnnotations.Schema;
using Sephiroth_DAO;

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
    public class {0}_DAO : BaseDAO<{0}>
    {{
        private Sephiroth_IDao.IDAO dao = new DapperDAO(Sephiroth_DAO.CreateDB_Connection.GetSephiroth_System());
        public override Sephiroth_IDao.IDAO absORM 
        {{
            get {{ return dao; }}
            set {{ dao = value; }}
        }}
", this.filename, PC_Helper.GetMachineName(), DateTime.Now.ToString("yyyy-MM-dd HH:mm"), this.fullname, this.remark ?? ""));

            
            sb.Append(string.Format(@"    }}
}}
"));
            string spatch = string.Format("{0}\\{1}_DAO.cs", this.fpatch, this.filename);
            using (System.IO.StreamWriter sw = new StreamWriter(spatch, false, System.Text.Encoding.UTF8))
            {
                sw.WriteLine(sb.ToString());
            }

        }
    }
}
