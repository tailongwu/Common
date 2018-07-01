using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using HttpCommon;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
             EmailServerTest();
        }

        static void EmailServerTest()
        {
            EmailServer email = new EmailServer("wu_tailong@sina.com", "wu_tailong@qq.com", "Love you", "I love you");
            email.SendEmail();
        }
    }
}
