using DevExpress_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
    * CLR 版本：       4.0.30319.33440
    * 类 名 称：       DXErrorProvider_Proxy
    * 机器名称：       SEPHIROTHBF0C
    * 命名空间：       SMB_CodeTools.Comm_Proxy
    * 文 件 名：       DXErrorProvider_Proxy
    * 创建时间：       15/4/5 13:40:11
    * 作    者：       吴占超
    * 说    明：       
    * 修改时间：
    * 修 改 人：
*************************************************************************************/

namespace CodeTools.Facade
{
    public class DXErrorProvider_Facade
    {
        /// <summary>
        /// 代理枚举 根据枚举创建具体实例
        /// </summary>
        public enum eProxy
        {
            DC_DXErrorProvider
        }

        /// <summary>
        /// 创建验证对象
        /// </summary>
        /// <param name="eproxy"></param>
        /// <returns></returns>
        public static I_DXErrorProvider CreateDXErrorProvider(eProxy eproxy)
        {
            return DevExpress_Common.ReflectionHelper.CreateInstance<I_DXErrorProvider>("DevExpress_Common", "DevExpress_Common", eproxy.ToString());
        }
    }
}
