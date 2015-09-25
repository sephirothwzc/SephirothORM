using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


/*************************************************************************************
    * CLR 版本：       4.0.30319.33440
    * 类 名 称：       MessageBox_Helper
    * 机器名称：       SEPHIROTHBF0C
    * 命名空间：       DevExpress_Common
    * 文 件 名：       MessageBox_Helper
    * 创建时间：       15/5/8 15:38:32
    * 作    者：       吴占超
    * 说    明：       
    * 修改时间：
    * 修 改 人：
*************************************************************************************/

namespace DevExpress_Common
{
    public class MessageBox_Helper
    {
        /// <summary>
        /// 确认提示框
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Title"></param>
        /// <param name="me"></param>
        /// <returns>确定true取消flase</returns>
        public static bool ConfirmMessage(string Message, string Title = "系统提示", MessageBoxButtons mbb = MessageBoxButtons.OKCancel)
        {
            DialogResult dr = MessageBox.Show(Message, Title,mbb,MessageBoxIcon.Information);
            return dr == DialogResult.OK || dr == DialogResult.Yes;
        }
    }
}
