using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SephirothCommon
{
    /// <summary>
    /// 反射帮助类
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <typeparam name="T">要创建对象的类型</typeparam>
        /// <param name="assemblyName">类型所在程序集名称</param>
        /// <param name="nameSpace">类型所在命名空间</param>
        /// <param name="className">类型名</param>
        /// <returns></returns>
        public static T CreateInstance<T>(string assemblyName, string nameSpace, string className)
        {
            try
            {
                string fullName = nameSpace + "." + className;//命名空间.类型名
                //此为第一种写法
                object ect = Assembly.Load(assemblyName).CreateInstance(fullName);//加载程序集，创建程序集里面的 命名空间.类型名 实例
                return (T)ect;//类型转换并返回
                //下面是第二种写法
                //string path = fullName + "," + assemblyName;//命名空间.类型名,程序集
                //Type o = Type.GetType(path);//加载类型
                //object obj = Activator.CreateInstance(o, true);//根据类型创建实例
                //return (T)obj;//类型转换并返回
            }
            catch
            {
                //发生异常，返回类型的默认值
                return default(T);
            }
        }

        /// <summary>
        /// 创建对象泛型实例
        /// </summary>
        /// <typeparam name="T">要创建对象的类型</typeparam>
        /// <param name="assemblyName">类型所在程序集名称</param>
        /// <param name="nameSpace">类型所在命名空间</param>
        /// <param name="className">类型名</param>
        /// <param name="types">泛型对象类型实例默认是1个将来做扩展</param>
        /// <returns></returns>
        public static T CreateInstanceT<T>(string assemblyName, string nameSpace, string className, Type types, object[] parameters=null)
        {
            Assembly assembly = Assembly.Load(assemblyName);

            string fullName = nameSpace + "." + className;//命名空间.类型名
            Type type = assembly.GetType(fullName + "`1");    //得到此类类型 注：（`1） 为占位符 不明确类型 1代表泛型类型 数量

            Type t = type.MakeGenericType(types);  //指定泛型类

            return (T)assembly.CreateInstance(t.FullName, true, System.Reflection.BindingFlags.Default, null, parameters, null, null);  //创建其实例
        }

    }
}
