using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HttpCommon
{
    public class EmailServer
    {
        private const string SPLIT_SEMCOLON = ",";

        // Email regex pattern
        private const string EMAIL_REGEX_USER_NAME = @"^\w+([-+.]\w+)*";

        private Configuration config;

        private string _emailFrom;
        private string _emailAddresses; // Split by comma
        private string _emailSubject;
        private string _emailBody;
        private string _emailHost;
        private string _emailUserName;
        private string _emailPassword;

        public EmailServer(
            string emailAddresses,
            string emailFrom = null,
            string emailSubject = null,
            string emailBody = null,
            string emailHost = null,
            string emailUserName = null,
            string emailPassword = null)
        {
            if (string.IsNullOrEmpty(emailAddresses))
            {
                throw new InvalidOperationException();
            }

            config = new Configuration();

            this._emailAddresses = emailAddresses;
            this._emailFrom = emailFrom;
            this._emailSubject = emailSubject;
            this._emailBody = emailBody;
            this._emailHost = emailHost;
            this._emailUserName = emailUserName;
            this._emailPassword = emailPassword;

            if (string.IsNullOrEmpty(emailFrom))
            {
                this._emailFrom = config.SinaEmailAddress;
            }
            if (string.IsNullOrEmpty(emailHost))
            {
                this._emailHost = config.SinaEmailHost;
            }
            if (string.IsNullOrEmpty(emailUserName))
            {
                this._emailUserName = this.GetUserNameFromEmailAddress(config.SinaEmailAddress);
            }
            if (string.IsNullOrEmpty(emailPassword))
            {
                this._emailPassword = config.SinaEmailPassword;
            }
        }

        public bool SendEmail()
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(this._emailFrom);
            mailMessage.To.Add(this._emailAddresses);
            mailMessage.Subject = this._emailSubject;
            mailMessage.Body = this._emailBody;

            SmtpClient client = new SmtpClient();
            client.Host = this._emailHost;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(this._emailUserName, this._emailPassword);
            client.Send(mailMessage);
            return true;
        }

        private string GetUserNameFromEmailAddress(string emailAddress)
        {
            return Helper.GetDestinationFromSource(emailAddress, EMAIL_REGEX_USER_NAME);
        }
    }
}
