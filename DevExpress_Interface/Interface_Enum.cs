using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
    * CLR 版本：       4.0.30319.33440
    * 类 名 称：       Interface_Enum
    * 机器名称：       SEPHIROTHBF0C
    * 命名空间：       DevExpress_Interface
    * 文 件 名：       Interface_Enum
    * 创建时间：       15/4/5 15:02:53
    * 作    者：       吴占超
    * 说    明：       接口包枚举对象适用于各种接口中参数的枚举值
    * 修改时间：
    * 修 改 人：
*************************************************************************************/

namespace DevExpress_Interface
{
    public class Interface_Enum
    {
        /// <summary>
        /// 验证类型
        /// </summary>
        public enum ValidateEnum
        {
            /// <summary>
            /// 非空
            /// </summary>
            NotEmpty,
            /// <summary>
            /// 非空且必须为数字 小数整数都可以
            /// </summary>
            NotE_Number,
            /// <summary>
            /// 非空且必须为小数(有小数点)
            /// </summary>
            NotE_decimal,
            /// <summary>
            /// 非空且必须为小数2位
            /// </summary>
            NotE_decimal2,
            /// <summary>
            /// 非空且必须为小数4位
            /// </summary>
            NotE_decimal4,
            /// <summary>
            /// 非空且必须为正整数
            /// </summary>
            NotE_PositiveInteger,
            /// <summary>
            /// 小数
            /// </summary>
            ISdecimal,
            /// <summary>
            /// 数字
            /// </summary>
            ISNumber,
            /// <summary>
            /// 正整数
            /// </summary>
            ISPositiveInteger
        }
    }
}
