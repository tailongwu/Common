using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Common
{
    public class Configuration
    {
        private const string CONFIG_PATH = @"D:\Configuration.xml";

        private string _sinaEmailAddress;
        private string _sinaEmailPassword;
        private string _sinaEmailHost;

        private string _tailongID;
        private string _tailongQQEmail;

        private string _xiyunQQEmail;

        public Configuration()
        {
            this.Initialize();
        }

        public string SinaEmailAddress
        {
            get
            {
                return this._sinaEmailAddress;
            }
        }

        public string SinaEmailPassword
        {
            get
            {
                return this._sinaEmailPassword;
            }
        }

        public string SinaEmailHost
        {
            get
            {
                return this._sinaEmailHost;
            }
        }

        public string TailongID
        {
            get
            {
                return this._tailongID;
            }
        }

        public string TailongQQEmail
        {
            get
            {
                return this._tailongQQEmail;
            }
        }

        public string XiyunQQEmail
        {
            get
            {
                return this._xiyunQQEmail;
            }
        }

        public void Initialize()
        {
            this._sinaEmailAddress = string.Empty;
            this._sinaEmailPassword = string.Empty;
            this._sinaEmailHost = string.Empty;

            XmlDocument config = Helper.LoadXML(CONFIG_PATH);
            XmlNode configurationNode = config.SelectSingleNode("configuration");

            // Initialize sina email info
            XmlNode sinaEmailInfoNode = configurationNode.SelectSingleNode("sinaEmailInfo");
            this._sinaEmailAddress = Helper.DecryptBase64(sinaEmailInfoNode.SelectSingleNode("account").InnerText);
            this._sinaEmailPassword = Helper.DecryptBase64(sinaEmailInfoNode.SelectSingleNode("password").InnerText);
            this._sinaEmailHost = Helper.DecryptBase64(sinaEmailInfoNode.SelectSingleNode("host").InnerText);

            // Initialize tailong's info
            XmlNode tailongInfoNode = configurationNode.SelectSingleNode("tailongInfo");
            this._tailongID = Helper.DecryptBase64(tailongInfoNode.SelectSingleNode("id").InnerText);
            this._tailongQQEmail = Helper.DecryptBase64(tailongInfoNode.SelectSingleNode("qqEmail").InnerText);

            // Initialize xiyun's info
            XmlNode xiyunInfoNode = configurationNode.SelectSingleNode("xiyunInfo");
            this._xiyunQQEmail = Helper.DecryptBase64(xiyunInfoNode.SelectSingleNode("qqEmail").InnerText);
        }
    }
}
