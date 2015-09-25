using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

/*************************************************************************************
  * CLR 版本：       4.0.30319.33440
  * 类 名 称：       BaseEntity
  * 机器名称：       SEPHIROTHBF0C
  * 命名空间：       Sephiroth_IDao
  * 文 件 名：       BaseEntity
  * 创建时间：       2015/09/22 10:30:53
  * 作    者：       吴占超
  * 说    明：
  * 修改时间：
  * 修 改 人：
 *************************************************************************************/

namespace Sephiroth_IDao
{
    /// <summary>
    /// VO base
    /// </summary>
    [Serializable]
    public class BaseEntity
    {
        /// <summary>
        /// 获取当前 TableName
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            var tn = this.GetType().GetCustomAttributes<TableAttribute>(false).FirstOrDefault();
            return tn == null ? this.GetType().Name : tn.Name;
        }

        /// <summary>
        /// 处理insert用的主键如果自增则不处理非自增且为空则赋值Guid 如果有多个主键为空只对名称为id属性赋值
        /// </summary>
        public void SetInsertKey()
        {
            var list = this.GetType().GetProperties().Where(p => PropertyKey(p) && !PropertyIdentity(p) && p.GetValue(this) == null).ToList();
            list.ForEach(l =>
            {
                l.SetValue(this, NewComb());//遍历赋值主键
            });
        }

        /// <summary>
        /// 是否主键
        /// </summary>
        /// <param name="pif"></param>
        /// <returns>true 主键</returns>
        public bool PropertyKey(PropertyInfo pif)
        {
            return pif.GetCustomAttribute<KeyAttribute>() != null;
        }

        /// <summary>
        /// 是否自增
        /// </summary>
        /// <param name="pif"></param>
        /// <returns>true 自增</returns>
        public bool PropertyIdentity(PropertyInfo pif)
        {
            var key = pif.GetCustomAttribute<DatabaseGeneratedAttribute>();
            return key != null && key.DatabaseGeneratedOption == DatabaseGeneratedOption.Identity;
        }

        /// <summary>
        /// 是否生成映射
        /// </summary>
        /// <param name="pif"></param>
        /// <returns>true 不生成映射</returns>
        public bool PropertyNotMapped(PropertyInfo pif)
        {
            return pif.GetCustomAttribute<NotMappedAttribute>() != null;
        }

        /// <summary>
        /// 属性列名
        /// </summary>
        /// <param name="pif"></param>
        /// <returns></returns>
        public ColumnAttribute PropertyColumn(PropertyInfo pif)
        {
            return pif.GetCustomAttribute<ColumnAttribute>();
        }

        #region NewComb--返回 GUID 用于数据库操作，特定的时间代码可以提高检索效率
        ////<summary> 
        /// 返回 GUID 用于数据库操作，特定的时间代码可以提高检索效率 
        /// </summary> 
        /// <returns>COMB (GUID 与时间混合型) 类型 GUID 数据</returns> 
        public static Guid NewComb()
        {
            byte[] guidArray = System.Guid.NewGuid().ToByteArray();
            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;
            // Get the days and milliseconds which will be used to build the byte string 
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = new TimeSpan(now.Ticks - (new DateTime(now.Year, now.Month, now.Day).Ticks));
            // Convert to a byte array 
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));
            // Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);
            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            return new System.Guid(guidArray);
        }
        #endregion

        #region GetDateFromComb--从 SQL SERVER 返回的 GUID 中生成时间信息
        /// <summary> 
        /// 从 SQL SERVER 返回的 GUID 中生成时间信息 
        /// </summary> 
        /// <param name="guid">包含时间信息的 COMB </param> 
        /// <returns>时间</returns> 
        public static DateTime GetDateFromComb(System.Guid guid)
        {

            DateTime baseDate = new DateTime(1900, 1, 1);
            byte[] daysArray = new byte[4];
            byte[] msecsArray = new byte[4];
            byte[] guidArray = guid.ToByteArray();
            // Copy the date parts of the guid to the respective byte arrays. 
            Array.Copy(guidArray, guidArray.Length - 6, daysArray, 2, 2);
            Array.Copy(guidArray, guidArray.Length - 4, msecsArray, 0, 4);
            // Reverse the arrays to put them into the appropriate order 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);
            // Convert the bytes to ints 
            int days = BitConverter.ToInt32(daysArray, 0);
            int msecs = BitConverter.ToInt32(msecsArray, 0);
            DateTime date = baseDate.AddDays(days);
            date = date.AddMilliseconds(msecs * 3.333333);
            return date;
        }
        #endregion
    }
}
