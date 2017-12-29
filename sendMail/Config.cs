using System.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace sendMail
{
    static class Config
    {
        public static string UserSMTP = "admin@vial.by";
        public static string PassSMTP = "vial2016";
        public static string fromSMTPmail = "admin@vial.by";
        public static string toSMTPmail = "";
        public static string serverSMTP = "mail.vial.by";
        public static bool isExistXMLConfig()
        {
            return File.Exists("Config.xml") ? true : false;
        }
        public static void SaveConfig()
        {
            using (XmlWriter wr = XmlWriter.Create("config.xml"))
            {
                wr.WriteStartDocument();
                wr.WriteStartElement("Settings");

                wr.WriteStartElement("UserSMTP");
                wr.WriteString(UserSMTP);
                wr.WriteEndElement();

                wr.WriteStartElement("PassSMTP");
                wr.WriteString(PassSMTP);
                wr.WriteEndElement();

                wr.WriteStartElement("serverSMTP");
                wr.WriteString(serverSMTP);
                wr.WriteEndElement();

                wr.WriteStartElement("toSMTPmail");
                wr.WriteString(toSMTPmail);
                wr.WriteEndElement();

                wr.WriteStartElement("fromSMTPmail");
                wr.WriteString(fromSMTPmail);

                wr.WriteEndElement();
                wr.WriteEndDocument();
                wr.Close();
            }
        }
        public static void ReadConfig()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("config.xml");
            XmlNodeList elem = doc.GetElementsByTagName("UserSMTP");
            UserSMTP = elem[0].InnerText;
            elem = doc.GetElementsByTagName("PassSMTP");
            PassSMTP = elem[0].InnerText;
            elem = doc.GetElementsByTagName("serverSMTP");
            serverSMTP = elem[0].InnerText;
            elem = doc.GetElementsByTagName("toSMTPmail");
            toSMTPmail = elem[0].InnerText;
            elem = doc.GetElementsByTagName("fromSMTPmail");
            fromSMTPmail = elem[0].InnerText;
            doc = null;
        }
    }
}
