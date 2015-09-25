using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevExpress_Interface
{
    /// <summary>
    /// DXErrorProvider interface
    /// </summary>
    public interface I_DXErrorProvider
    {
        /// <summary>
        /// 验证控件绑定非空
        /// </summary>
        /// <param name="con">验证控件</param>
        /// <param name="ve">验证类型,默认非空</param>
        /// <param name="errorText">提示文本 默认为'必填项'</param>
        /// <returns>绑定即会验证返回验证是否通过 true 没有错误 false 有错误</returns>
        bool ValidateControl(Control con, Interface_Enum.ValidateEnum ve = Interface_Enum.ValidateEnum.NotEmpty, string errorText = "必填项");

        /// <summary>
        /// 取消控件验证
        /// </summary>
        /// <param name="con">默认为null 取消所有控件验证</param>
        void ClearControl(Control con=null);

        /// <summary>
        /// 是否包含验证未通过的控件
        /// </summary>
        /// <param name="checkall">是否验证后返回未通过控件 默认true 验证</param>
        /// <returns></returns>
        List<Control> HasError(bool checkall = true);

        /// <summary>
        /// 指定控件错误文本
        /// </summary>
        /// <param name="con"></param>
        /// <param name="errMessage"></param>
        void SetControlError(Control con, string errMessage);
    }
}
