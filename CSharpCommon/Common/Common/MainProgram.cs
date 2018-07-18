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
