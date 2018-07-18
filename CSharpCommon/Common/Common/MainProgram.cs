using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            for (int i = 0; i < 5; i++)
            Logger.WriteLog("This is a test" + i);
           // EmailServerTest();
           //  HouseRankingTest();
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
    }
}
