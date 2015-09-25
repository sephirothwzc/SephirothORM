using CodeTools.Facade;
using DevExpress_Interface;
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
    /// entity生成设置窗体
    /// </summary>
    public partial class CreateSetting : Form
    {
        #region 私有变量
        /// <summary>
        /// 验证对象
        /// </summary>
        private I_DXErrorProvider idp;
        #endregion

        public CreateSetting()
        {
            InitializeComponent();
            //初始化验证对象
            idp = DXErrorProvider_Facade.CreateDXErrorProvider(DXErrorProvider_Facade.eProxy.DC_DXErrorProvider);
        }

        public object rfpatch { get; set; }

        public string rfilename { get; set; }

        public string rproname { get; set; }

        private void CreateSetting_Load(object sender, EventArgs e)
        {
            this.InitData();
            this.ValidateControl();
        }

        /// <summary>
        /// 验证
        /// </summary>
        private void ValidateControl()
        {
            this.idp.ValidateControl(this.fpatch);
            this.idp.ValidateControl(this.fullname);
        }

        /// <summary>
        /// 加载基础数据
        /// </summary>
        private void InitData()
        {
            this.fpatch.Text = WinCommon.ConfigHelper.GetCfgValue(this.fpatch.Name);
            this.filename.Text = WinCommon.ConfigHelper.GetCfgValue(this.filename.Name);
            this.proname.Text = WinCommon.ConfigHelper.GetCfgValue(this.proname.Name);
            this.fullname.Text = WinCommon.ConfigHelper.GetCfgValue(this.fullname.Name);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.idp.HasError().Count() > 0)
                return;
            WinCommon.ConfigHelper.SetCfgValue(this.fpatch.Name,this.fpatch.Text);
            WinCommon.ConfigHelper.SetCfgValue(this.filename.Name,this.filename.Text);

            WinCommon.ConfigHelper.SetCfgValue(this.fullname.Name,this.fullname.Text);
            this.rfpatch = this.fpatch.Text;
            this.rfilename = this.filename.Text;
            this.rproname = this.proname.Text;
            this.Close();
        }

        private void fpatch_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                this.fpatch.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
