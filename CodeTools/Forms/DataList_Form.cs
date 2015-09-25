using CodeTools.Entity;
using CodeTools.Interfaces;
using DevExpress_Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeTools
{
    /// <summary>
    /// 对象详细窗体
    /// </summary>
    public partial class DataList_Form : Form
    {
        private IEnumerable<TableObject> tableobj { get; set; } 
        public DataList_Form()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 有参数构造函数
        /// </summary>
        /// <param name="objid">表or视图 id</param>
        public DataList_Form(string objid)
            : this()
        {
            CodeTools.Interfaces.IDB_DAO db = new MSSQLDB_DAO<TableObject>(Login.dao);

            tableobj = db.GetTableObject(objid);
        }

        private void DataList_Form_Load(object sender, EventArgs e)
        {
            this.gridControl1.DataSource = tableobj;
        }
    }
}
