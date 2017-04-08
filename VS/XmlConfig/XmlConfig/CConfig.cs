using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace XmlConfig
{
    class CConfig
    {
        //数据库配置信息
        public static string ConnString = "";
        //SMTP发信账号信息
        public static string SmtpIp = "";
        public static string SmtpUser = "";
        public static string SmtpPass = "";

        /// <summary>
        /// 一次性读取配置文件
        /// </summary>
        public static void LoadConfig()
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                string xmlfile = System.Windows.Forms.Application.StartupPath+"\\xml.xml";
                if (!File.Exists(xmlfile))
                {
                    throw new Exception("配置文件不存在，路径：" + xmlfile);
                }
                xml.Load(xmlfile);
                string tmpValue = null;
                //数据库连接字符串
                if (xml.GetElementsByTagName("connstring").Count > 0)
                {
                    tmpValue = xml.DocumentElement["connstring"].InnerText.Trim();
                    CConfig.ConnString = tmpValue;
                }
                //smtp
                if (xml.GetElementsByTagName("smtpip").Count > 0)
                {
                    tmpValue = xml.DocumentElement["smtpip"].InnerText.Trim();
                    CConfig.SmtpIp = tmpValue;
                }
                if (xml.GetElementsByTagName("smtpuser").Count > 0)
                {
                    tmpValue = xml.DocumentElement["smtpuser"].InnerText.Trim();
                    CConfig.SmtpUser = tmpValue;
                }
                if (xml.GetElementsByTagName("smtppass").Count > 0)
                {
                    tmpValue = xml.DocumentElement["smtppass"].InnerText.Trim();
                    CConfig.SmtpPass = tmpValue;
                }
            }
            catch (Exception ex)
            {
               // CConfig.SaveLog("CConfig.LoadConfig() fail,error:" + ex.Message);
                Environment.Exit(-1);
            }
        }
    }
}
