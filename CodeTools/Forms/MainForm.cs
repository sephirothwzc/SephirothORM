using Commonly;
using CodeTools.Facade;
using CodeTools.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/*************************************************************************************
    * CLR 版本：       4.0.30319.33440
    * 类 名 称：       MainForm
    * 机器名称：       SEPHIROTHBF0C
    * 命名空间：       DapperTools
    * 文 件 名：       MainForm
    * 创建时间：       15/4/28 18:59:35
    * 作    者：       吴占超
    * 说    明：       
    * 修改时间：
    * 修 改 人：
*************************************************************************************/

namespace CodeTools
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// dao操作类
        /// </summary>
        private DB_DAO_Facade dbfacade = new DB_DAO_Facade(Login.dao);

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.documentManager1.MdiParent = this;
            this.InitData();
        }

        /// <summary>
        /// 初始化数据源
        /// </summary>
        private void InitData()
        {
            this.gridControl1.DataSource = this.dbfacade.IDB_DAO_GetDBTree();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.InitData();
        }

        /// <summary>
        /// 双击查看加载当前选中行的数据结构
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle < 0)
                return;
            string objid = this.gridView1.GetFocusedRowCellValue(this.col_表名).ToString();//获取id
            string formtext = objid+"_数据字典";
            Form frm = (from f in this.MdiChildren
                       where f.Text.Equals(formtext)
                       select f).FirstOrDefault();
            if (frm == null)
            {
                DataList_Form df = new DataList_Form(objid);
                df.Text = formtext;
                df.MdiParent = this;
                df.Show();
            }
            else
                frm.Activate();
        }

        /// <summary>
        /// 选择数据源循环生成文件Entity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() < 0)
                return;
            CreateSetting cs = new CreateSetting();
            cs.ShowDialog();
            if (cs.rfpatch == null)
                return;

            DB_DAO_Facade dbfacade = new DB_DAO_Facade(Login.dao);
            foreach (int rindex in this.gridView1.GetSelectedRows())
            {
                string tablename = this.gridView1.GetRowCellValue(rindex,this.col_表名).ToString();
                var plist = this.dbfacade.IDB_DAO_GetTableObject(tablename);
                string filename = string.Format(cs.rfilename==""?tablename:string.Format(cs.rfilename,tablename));
                object rem = this.gridView1.GetRowCellValue(rindex, this.col_说明);
                CreateEntity ce = new CreateEntity(plist, cs.rfpatch.ToString(), filename,remark:rem==null?string.Empty: rem.ToString());
                ce.CreateEntityRUN();
            }
            MessageBox.Show("文件生成成功！");
        }

        /// <summary>
        /// 选择数据源循环生成DAO文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.gridView1.GetSelectedRows().Count() < 0)
                return;
            //DAO文件路径
            string daopatch = string.Empty;
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                daopatch = folderBrowserDialog.SelectedPath;
            }
            else
                return;

            DB_DAO_Facade dbfacade = new DB_DAO_Facade(Login.dao);
            foreach (int rindex in this.gridView1.GetSelectedRows())
            {
                string tablename = this.gridView1.GetRowCellValue(rindex, this.col_表名).ToString();
                var plist = this.dbfacade.IDB_DAO_GetTableObject(tablename);
                //string filename = string.Format(cs.rfilename == "" ? tablename : string.Format(cs.rfilename, tablename));
                string filename = tablename;
                object rem = this.gridView1.GetRowCellValue(rindex, this.col_说明);
                CreateDAO ce = new CreateDAO(plist, daopatch, filename, remark: rem == null ? string.Empty : rem.ToString());
                ce.CreateEntityRUN();
            }
            MessageBox.Show("DAO文件生成成功！");
        }
    }
}
