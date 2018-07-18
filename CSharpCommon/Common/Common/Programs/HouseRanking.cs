using Common;
using HttpCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Programs
{
    class HouseRanking
    {
        private const string DEFAULT_RANKING = "【-1】";
        private const string URL = "http://ent.sipmch.sipac.gov.cn/ModuleDefaultCompany/RentManage/SearchRentNo";
        private const string SEARCH_POST_BODY = "CertNo={0}";
        private const string REGEX_RANKING = @"[【]\d+[】]";

        private const string EMAIL_SUBJECT = "当前菁英公寓排名：{0}";
        
        private static string Last_Query_Ranking;

        private Configuration _config;
        private string _emailAddresses;

        public HouseRanking()
        {
            this._config = new Configuration();
            this._emailAddresses = string.Format("{0},{1}", this._config.XiyunQQEmail,this._config.TailongQQEmail);
            Last_Query_Ranking = DEFAULT_RANKING;
        }

        public void Run()
        {
            while (true)
            {
                string ranking = GetCurrentHouseRanking();
                if (!ranking.Equals(Last_Query_Ranking))
                {
                    Last_Query_Ranking = ranking;
                    SendEmailWithHouseRanking(ranking);
                }
                Thread.Sleep(1000 * 60 * 60);
            }
        }

        public void SendEmailWithHouseRanking(string ranking)
        {
            string emailSubject = string.Format(EMAIL_SUBJECT, ranking);
            EmailServer emailServer = new EmailServer(this._emailAddresses, null, emailSubject);
            emailServer.SendEmail();
        }

        public string GetCurrentHouseRanking()
        {
            string result = DEFAULT_RANKING;
            string html = FetchHouseRanking();
            result = Common.Helper.GetDestinationFromSource(html, REGEX_RANKING);
            return result;
        }

        public string FetchHouseRanking()
        {
            string body = string.Format(SEARCH_POST_BODY, this._config.TailongID);
            return HttpCommon.HttpHelper.ReadHttpWebResponse(HttpCommon.HttpHelper.POSTRequest(URL, body));
        }
    }
}
