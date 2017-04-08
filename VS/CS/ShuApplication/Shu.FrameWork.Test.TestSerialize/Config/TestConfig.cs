using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using Shu.FrameWork.Util.Serialize;

namespace Shu.FrameWork.Test.TestSerialize.Config
{
    [XmlRoot("ShuAppConfig")]
    [Serializable]
    public class TestConfig
    {
        #region 对象属性
        [XmlElement(ElementName = "AppName")]
        public string AppName { get; set; }
        #endregion
        #region 对象配置
        private const string File_Name = @"\xml\TestConfig.config";
        private static string m_strFilePath = string.Format("{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory, File_Name);
        private static TestConfig m_pTestConfig;
        #endregion
        #region 对象方法
        public static bool Read(string filePath)
        {
            m_strFilePath = filePath;
            if (string.IsNullOrEmpty(m_strFilePath))
            {
                return false;
            }
            if (!File.Exists(m_strFilePath))
            {
                return false;
            }
            m_pTestConfig = XmlSerialize.XmlToSerialize<TestConfig>(m_strFilePath);
            if (m_pTestConfig == null)
            {
                return false;
            }
            return true;
        }

        public static bool Read()
        {
            if (m_pTestConfig != null)
            {
                return true;
            }
            return Read(m_strFilePath);
        }

        public static TestConfig Current
        {
            get
            {
                if (!Read())
                {
                    return null;
                }
                return m_pTestConfig;
            }
        }

        public static bool Write()
        {
            if (m_pTestConfig==null)
            {
                return false;
            }
            return Write(m_pTestConfig);
        }

        private static bool Write(TestConfig config)
        {
            XmlSerialize.SerializeToXml<TestConfig>(m_strFilePath, m_pTestConfig);
            return true;
        }
        #endregion


    }
}
