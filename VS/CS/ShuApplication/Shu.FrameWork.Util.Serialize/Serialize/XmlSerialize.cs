using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Shu.FrameWork.Util.Serialize
{
    public class XmlSerialize
    {
        
        public static T XmlToSerialize<T>(string configPath) {
            FileStream fs = null;
            T obj = default(T);
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                fs = new FileStream(configPath, FileMode.Open, FileAccess.Read);
                obj = (T)xs.Deserialize(fs);
                fs.Close();
                return obj;
            }
            catch (System.Exception ex)
            {
            	if (fs!=null)
            	{
                    fs.Close();
            	}
                Trace.TraceError(ex.Message);
                throw ex;
            }
        }

        public static void SerializeToXml<T>(string Path, T obj)where T:class
        {
            FileStream fs = null;
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                fs = new FileStream(Path, FileMode.Create, FileAccess.Write);
                xs.Serialize(fs,obj);
                obj = null;
                fs.Close();
            }
            catch (System.Exception ex)
            {
            	if (fs!=null)
            	{
                    fs.Close();
            	}
            }
        
        }
      
    }
}
