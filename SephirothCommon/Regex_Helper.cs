using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


/*************************************************************************************
    * CLR 版本：       4.0.30319.33440
    * 类 名 称：       Regex_Helper
    * 机器名称：       SEPHIROTHBF0C
    * 命名空间：       Commonly
    * 文 件 名：       Regex_Helper
    * 创建时间：       15/4/15 19:55:46
    * 作    者：       吴占超
    * 说    明：       正则表达式验证帮助类
    * 修改时间：
    * 修 改 人：
*************************************************************************************/

namespace SephirothCommon
{
    /// <summary>
    /// 正则表达式验证
    /// </summary>
    public class Regex_Helper
    {
        /// <summary>
        /// 验证是否匹配
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static bool RegexIsMatch(string text, string pattern)
        {
            //正则验证
            Regex reg = new Regex(pattern);
            return reg.IsMatch(text);
        }

        /// <summary>
        /// 非空且必须为小数(有小数点)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool Decimal(string text)
        {
            return RegexIsMatch(text, @"^-?\d+\.\d+$");
        }

        /// <summary>
        /// 有理数 小数整数负数都可以
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool Number(string text)
        {
            return RegexIsMatch(text, @"^-?\d+(\.\d+)?$");
        }

        /// <summary>
        /// 小数(X位)默认2位
        /// </summary>
        /// <param name="text"></param>
        /// <param name="len">小数位数</param>
        /// <param name="tolen">如果是区间则需要同tolen 配合 len默认是0 固定位数</param>
        /// <returns></returns>
        public static bool DecimalLenth(string text,int len = 2,int tolen = 0)
        {
            return RegexIsMatch(text, string.Format(@"^-?\d+(\.\d{{{0}{1}}})?$", len, tolen == 0 ? "" : "," + tolen));
        }

        /// <summary>
        /// 正整数
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool PositiveInteger(string text)
        {
            return RegexIsMatch(text, @"\d+$");
        }
    }
}
