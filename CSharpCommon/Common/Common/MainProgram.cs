using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using HttpCommon;
using Programs;

namespace Test
{
    class MainProgram
    {
        static void Main(string[] args)
        {

            // EmailServerTest();
            // HouseRankingTest();
            // WriteLogTest();

            for (int i = 13; i < 1000; i++)
            {
                string url = "http://www.gebipuzi.cn/exitentry/zxkf_{0}.htm";
                url = string.Format(url, i.ToString());
                // Console.WriteLine(url);
                string html = HttpHelper.ReadHttpWebResponse(HttpHelper.GETRequest(url));
                if (html.Contains("L签"))
                {
                    Console.WriteLine(i);
                }
            }
        }

        static void EmailServerTest()
        {
            EmailServer email = new EmailServer("wu_tailong@sina.com", "wu_tailong@qq.com", "Test", "This is a test.");
            email.SendEmail();
        }

        static void HouseRankingTest()
        {
            HouseRanking rank = new HouseRanking();
            rank.Run();
        }

        static void WriteLogTest()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(Log));
                thread.Start(i);
            }
        }

        static void Log(object info)
        {
            Logger.WriteLog(info.ToString());
        }
    }
}
