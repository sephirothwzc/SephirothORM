using SephirothCommon;
using DevExpress_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


/*************************************************************************************
    * CLR 版本：       4.0.30319.33440
    * 类 名 称：       DC_DXErrorProvider
    * 机器名称：       SEPHIROTHBF0C
    * 命名空间：       DevExpress_Common.DC_Class
    * 文 件 名：       DC_DXErrorProvider
    * 创建时间：       15/3/9 16:34:34
    * 作    者：       吴占超
    * 说    明：       
    * 修改时间：
    * 修 改 人：
*************************************************************************************/

namespace DevExpress_Common
{
    public class DC_DXErrorProvider : I_DXErrorProvider
    {

        #region 私有变量
        /// <summary>
        /// 验证控件
        /// </summary>
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxep = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();

        /// <summary>
        /// 验证控件列表
        /// </summary>
        private List<ConErrText> l_NEcon = new List<ConErrText>();
        #endregion 

        #region 接口实现
        /// <summary>
        /// 验证控件绑定非空
        /// </summary>
        /// <param name="con">控件</param>
        bool I_DXErrorProvider.ValidateControl(Control con, Interface_Enum.ValidateEnum ve , string errorText)
        {
            //判断当前对象控件是否已经存在
            var cet = this.l_NEcon.Where(c => con.Equals(c.con)).SingleOrDefault();
            if (cet == null)
            {
                cet = new ConErrText
                {
                    con = con,
                    ve = ve,
                    errText = errorText
                };
                con.Validating += con_Validating;
                this.l_NEcon.Add(cet);
            }
            else
            {
                if (cet.ve != ve)
                    cet.ve = ve;
                else if (cet.errText != errorText)
                    cet.errText = errorText;
            }

            return this.ValidateRun(cet);
        }

        /// <summary>
        /// 取消控件验证
        /// </summary>
        /// <param name="con">默认为null 取消所有控件验证</param>
        void I_DXErrorProvider.ClearControl(System.Windows.Forms.Control con)
        {
            this.l_NEcon.Remove(this.l_NEcon.SingleOrDefault(p=>con.Equals(p.con)));
        }

        /// <summary>
        /// 是否包含验证未通过的控件
        /// </summary>
        /// <param name="checkall">是否验证后返回未通过控件 默认true 验证</param>
        /// <returns></returns>
        List<System.Windows.Forms.Control> I_DXErrorProvider.HasError(bool checkall)
        {
            return this.l_NEcon.Where(c => !this.ValidateRun(c)).Select(c => c.con).ToList();
        }

        public void SetControlError(Control con, string errMessage)
        {
            this.dxep.SetError(con, errMessage);
        }
        #endregion 

        #region 事件
        /// <summary>
        /// 控件验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void con_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = this.l_NEcon.Where(c => (sender as Control).Equals(c.con)
                && !this.ValidateRun(c)).Count() > 0;
        }
        #endregion 

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="cet"></param>
        /// <returns>true 验证成功 false 验证失败</returns>
        private bool ValidateRun(ConErrText cet)
        {
            if (cet == null)
                return true;// 为空则返回正确

            //如果 需要非空,首先验证非空
            if (cet.ve == Interface_Enum.ValidateEnum.NotEmpty)
            {
                if (string.IsNullOrWhiteSpace(cet.con.Text))
                {
                    this.dxep.SetError(cet.con, cet.errText);
                    return false;
                }
                else
                {
                    this.dxep.SetError(cet.con, string.Empty);
                    return true;
                }
            }
            if( cet.ve.ToString().Length > 4 && cet.ve.ToString().Substring(0, 4) == "NotE")
            {
                if (string.IsNullOrWhiteSpace(cet.con.Text))
                {
                    this.dxep.SetError(cet.con, cet.errText);
                }
                else
                {
                    this.dxep.SetError(cet.con, string.Empty);
                }
            }
            switch (cet.ve)
            { 
                case Interface_Enum.ValidateEnum.NotE_decimal:
                    decimal dd = 0M;
                    return decimal.TryParse(cet.con.Text,out dd);
                    //return Regex_Helper.Decimal(cet.con.Text);
                case Interface_Enum.ValidateEnum.NotE_Number:
                    return Regex_Helper.Number(cet.con.Text);
                case Interface_Enum.ValidateEnum.NotE_decimal2:
                    return Regex_Helper.DecimalLenth(cet.con.Text,2);
                case Interface_Enum.ValidateEnum.NotE_decimal4:
                    return Regex_Helper.DecimalLenth(cet.con.Text, 4);
                case Interface_Enum.ValidateEnum.NotE_PositiveInteger:
                    return Regex_Helper.PositiveInteger(cet.con.Text);
            }
            
            return false;
        }

    }

    /// <summary>
    /// 辅助model
    /// </summary>
    public class ConErrText
    {
        #region  属性
        /// <summary>
        /// 控件
        /// </summary>
        public Control con { get; set; }
        /// <summary>
        /// 错误提示文本
        /// </summary>
        public string errText { get; set; }
        /// <summary>
        /// 验证类型
        /// </summary>
        public Interface_Enum.ValidateEnum ve { get; set; }
        #endregion 
    }
}
