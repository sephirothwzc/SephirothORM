using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


/*************************************************************************************
    * CLR 版本：       4.0.30319.33440
    * 类 名 称：       ConfigHelper
    * 机器名称：       SEPHIROTHBF0C
    * 命名空间：       Commonly
    * 文 件 名：       ConfigHelper
    * 创建时间：       15/4/29 21:48:37
    * 作    者：       吴占超
    * 说    明：       
    * 修改时间：
    * 修 改 人：
*************************************************************************************/

namespace WinCommon
{
    public static class ConfigHelper
    {
        /// <summary>
        /// 是否包含配置节
        /// </summary>
        /// <param name="AppKey"></param>
        /// <returns></returns>
        public static bool HaveAppKey(string AppKey)
        {
            return System.Configuration.ConfigurationManager.AppSettings[AppKey] != null;
        }

        /// <summary>
        /// 读取App.config
        /// </summary>
        /// <param name="AppKey">string键名</param>
        /// <returns>string</returns>
        public static string GetCfgValue(string AppKey)
        {
            return System.Configuration.ConfigurationManager.AppSettings.Get(AppKey);
        }

        /// <summary>
        /// 写，引入 System.XML
        /// </summary>
        /// <param name="AppKey">Key 键名</param>
        /// <param name="AppValue">AppValue 内容</param>
        public static void SetCfgValue(string AppKey, string AppValue)
        {
            System.Xml.XmlDocument xDoc = new XmlDocument();
            xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");
            //xDoc.Load( System.IO.Path.GetFullPath("App.config"));

            XmlNode xNode;
            XmlElement xElemKey;
            XmlElement xElemValue;

            xNode = xDoc.SelectSingleNode("//appSettings");

            xElemKey = (XmlElement)xNode.SelectSingleNode("//add[@key=\"" + AppKey + "\"]");
            if (xElemKey != null)
                xElemKey.SetAttribute("value", AppValue);
            else
            {
                xElemValue = xDoc.CreateElement("add");
                xElemValue.SetAttribute("key", AppKey);
                xElemValue.SetAttribute("value", AppValue);
                xNode.AppendChild(xElemValue);
            }
            xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");
        }

        /// <summary>
        /// 修改webservices 配置节链接 URL
        /// </summary>
        /// <param name="AppName">链接字节name</param>
        /// <param name="AppValueUrl">链接字节URL</param>
        /// <param name="Xianmu">项目名称-子节点命名</param>
        public static void SetServiceCfgValue(string AppName, string AppValueUrl, string Xianmu)
        {
            System.Xml.XmlDocument xDoc = new XmlDocument();
            xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");


            XmlNode node = xDoc.SelectSingleNode(string.Format("//{0}.Properties.Settings", Xianmu));

            foreach (XmlNode xn in node.ChildNodes)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.GetAttribute("name").ToLower() == AppName.ToLower())
                {
                    xe.InnerText = AppValueUrl;
                    break;
                }
            }
            xDoc.Save(System.Windows.Forms.Application.ExecutablePath + ".config");
        }

        /// <summary>
        /// 获取webservices配置节的URL
        /// </summary>
        /// <param name="AppName">获取节点的名称</param>
        /// <param name="Xianmu">项目名称-子节点命名</param>
        /// <returns></returns>
        public static string GetServiceCfgValue(string AppName, string Xianmu)
        {
            System.Xml.XmlDocument xDoc = new XmlDocument();
            xDoc.Load(System.Windows.Forms.Application.ExecutablePath + ".config");

            XmlNode node = xDoc.SelectSingleNode(string.Format("//{0}.Properties.Settings", Xianmu));

            foreach (XmlNode xn in node.ChildNodes)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.GetAttribute("name").ToLower() == AppName.ToLower())
                {
                    return xe.InnerText.ToString();
                }
            }
            return "";
        }
    }
}
