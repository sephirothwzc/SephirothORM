using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
   * CLR 版本：       4.0.30319.33440
   * 类 名 称：       PC_Helper
   * 机器名称：       SEPHIROTHBF0C
   * 命名空间：       Commonly
   * 文 件 名：       PC_Helper
   * 创建时间：       15/5/29 11:27:52
   * 作    者：       吴占超
   * 说    明：       硬件配置获取信息 
   * 修改时间：
   * 修 改 人：
  *************************************************************************************/

namespace SephirothCommon
{
    /// <summary>
    /// 硬件配置帮助类
    /// </summary>
    public class PC_Helper
    {
        /// <summary>
        /// 获取计算机名称
        /// </summary>
        /// <returns></returns>
        public static string GetMachineName()
        {
            try
            {
                return System.Environment.MachineName;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return "uMnNk";
            }
        }
    }
}
