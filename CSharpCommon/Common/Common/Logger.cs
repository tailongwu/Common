using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Logger
    {
        const string LOG_PATH = @"D:\Logs";

        private static DateTime latestFileTime = DateTime.UtcNow;
        private static string logFilePath = string.Empty;

        public static void WriteLog(string logInfo)
        {
            UpdateLogFilePath();
            Helper.AppendToFile(logFilePath, 
                string.Format(
                    "{0} {1}",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    logInfo));
        }
        
        private static void UpdateLogFilePath()
        {
            if (!Helper.CheckIfFolderExists(LOG_PATH))
            {
                Helper.CreateFolder(LOG_PATH);
            }

            if (string.IsNullOrEmpty(logFilePath))
            {
                latestFileTime = DateTime.UtcNow;
                logFilePath = string.Format("{0}\\{1}.txt", LOG_PATH, GetTimeString(latestFileTime));
            }
            else if (latestFileTime.AddHours(1) < DateTime.UtcNow)
            {
                latestFileTime = DateTime.UtcNow;
                logFilePath = string.Format("{0}\\{1}.txt", LOG_PATH, GetTimeString(latestFileTime));
            }

            if (!Helper.CheckIfFileExists(logFilePath))
            {
                Helper.CreateFile(logFilePath);
            }
        }

        private static string GetTimeString(DateTime time)
        {
            string timeString = time.ToString();
            return timeString.Replace(' ', '_').Replace(':', '_').Replace('/', '_');
        }
    }
}
