using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress_Interface;
using DevExpress_Common;
using Sephiroth_IDao;
using CodeTools.Facade;
/*************************************************************************************
    * CLR 版本：       4.0.30319.33440
    * 类 名 称：       Login
    * 机器名称：       SEPHIROTHBF0C
    * 命名空间：       DapperTools
    * 文 件 名：       Login
    * 创建时间：       15/4/28 18:59:22
    * 作    者：       吴占超
    * 说    明：       
    * 修改时间：
    * 修 改 人：
*************************************************************************************/

namespace CodeTools
{
    public partial class Login : Form
    {
        #region 私有变量
        /// <summary>
        /// 验证对象
        /// </summary>
        private I_DXErrorProvider idp;
        #endregion
        /// <summary>
        /// 静态数据连接
        /// </summary>
        public static DB_Connection dao = new DB_Connection();

        public Login()
        {
            InitializeComponent();
            //初始化验证对象
            idp = DXErrorProvider_Facade.CreateDXErrorProvider(DXErrorProvider_Facade.eProxy.DC_DXErrorProvider);

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (idp.HasError().Count()>0)
                return;

            WinCommon.ConfigHelper.SetCfgValue(this.dbsource.Name,this.dbsource.Text);
            WinCommon.ConfigHelper.SetCfgValue(this.dbtype.Name,this.dbtype.Text);
            WinCommon.ConfigHelper.SetCfgValue(this.datasource.Name,this.datasource.Text);
            WinCommon.ConfigHelper.SetCfgValue(this.username.Name,this.username.Text);
            WinCommon.ConfigHelper.SetCfgValue(this.password.Name,this.password.Text);
            //验证
            dao.datasource = this.datasource.Text;
            dao.dbsource = this.dbsource.Text;
            dao.dbtype = (DB_Connection.e_DBType)Enum.Parse(typeof(DB_Connection.e_DBType), (this.dbtype.EditValue ?? "MSSQL").ToString());
            dao.username = this.username.Text;
            dao.password = this.password.Text;
            this.Hide();
            MainForm mf = new MainForm();
            mf.ShowDialog();
            Application.Exit();
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Load(object sender, EventArgs e)
        {

            //测试用demo
            //new MSSQL_Helper().Sql_Insert(new AA_AuthClass());
            this.InitData();
            this.ValidateControl();
        }

        private void SetSB(int mf)
        {
            mf = int.MinValue;
        }

        private void ValidateControl()
        {
            this.idp.ValidateControl(this.dbtype);
            this.idp.ValidateControl(this.datasource);
            this.idp.ValidateControl(this.username);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void InitData()
        {
            this.dbsource.Text = WinCommon.ConfigHelper.GetCfgValue(this.dbsource.Name);
            this.dbtype.Text = WinCommon.ConfigHelper.GetCfgValue(this.dbtype.Name);
            this.datasource.Text = WinCommon.ConfigHelper.GetCfgValue(this.datasource.Name);
            this.username.Text = WinCommon.ConfigHelper.GetCfgValue(this.username.Name);
            this.password.Text = WinCommon.ConfigHelper.GetCfgValue(this.password.Name);
        }
    }
}
