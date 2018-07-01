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
        private const string STRING_EMPTY = "";
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
            string emailFrom,
            string emailAddresses,
            string emailSubject = STRING_EMPTY,
            string emailBody = STRING_EMPTY,
            string emailHost = STRING_EMPTY,
            string emailUserName = STRING_EMPTY,
            string emailPassword = STRING_EMPTY)
        {
            config = new Configuration();

            this._emailFrom = emailFrom;
            this._emailAddresses = emailAddresses;
            this._emailSubject = emailSubject;
            this._emailBody = emailBody;

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
