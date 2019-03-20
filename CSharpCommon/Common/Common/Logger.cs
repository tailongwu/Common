using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class Logger
    {
        // Save log action queue
        private Queue<Action> _queue;

        // Thread to write log
        private Thread _loggingThread;

        // Signal of new log
        private ManualResetEvent _hasNew;

        private Logger()
        {
            _queue = new Queue<Action>();
            _hasNew = new ManualResetEvent(false);
            _loggingThread = new Thread(Process);
            _loggingThread.IsBackground = true;
            _loggingThread.Start();
        }

        private static readonly Object lockObject = new Object();
        private static Logger _logger = null;
        private static Logger GetInstance()
        {
            if (_logger == null)
            {
                lock (lockObject)
                {
                    if (_logger == null)
                    {
                        _logger = new Logger();
                    }
                }
            }
            return _logger;
        }

        private void Process()
        {
            while (true)
            {
                _hasNew.WaitOne();

                _hasNew.Reset();

                Queue<Action> copyQueue;
                lock (_queue)
                {
                    copyQueue = new Queue<Action>(_queue);
                    _queue.Clear();
                }

                foreach (var action in copyQueue)
                {
                    action();
                }
            }
        }

        private void WriteLog(string content)
        {
            lock(_queue)
            {
                _queue.Enqueue(() => Helper.AppendToFile("", content));
            }

            _hasNew.Set();
        }

        public static void Write(string content)
        {
            Task.Run(() => GetInstance().WriteLog(content));
        }
    }

    //public static class Logger
    //{
    //    const string LOG_PATH = @"D:\Logs";
    //    const int INTERNAL_MINUTES = 60;

    //    private static DateTime latestFileTime = DateTime.UtcNow;
    //    private static string logFilePath = string.Empty;
        
    //    public static void WriteLog(string logInfo)
    //    {
    //        lock(logFilePath)
    //        {
    //            UpdateLogFilePath();
    //            Helper.AppendToFile(logFilePath,
    //                string.Format(
    //                    "{0} {1}",
    //                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
    //                    logInfo));
    //        }
    //    }
        
    //    private static void UpdateLogFilePath()
    //    {
    //        if (!Helper.CheckIfFolderExists(LOG_PATH))
    //        {
    //            Helper.CreateFolder(LOG_PATH);
    //        }

    //        if (string.IsNullOrEmpty(logFilePath))
    //        {
    //            latestFileTime = DateTime.UtcNow;
    //            logFilePath = string.Format("{0}\\{1}.txt", LOG_PATH, GetTimeString(latestFileTime));
    //        }
    //        else if (latestFileTime.AddMinutes(INTERNAL_MINUTES) < DateTime.UtcNow)
    //        {
    //            latestFileTime = DateTime.UtcNow;
    //            logFilePath = string.Format("{0}\\{1}.txt", LOG_PATH, GetTimeString(latestFileTime));
    //        }

    //        if (!Helper.CheckIfFileExists(logFilePath))
    //        {
    //            Helper.CreateFile(logFilePath);
    //        }
    //    }

    //    private static string GetTimeString(DateTime time)
    //    {
    //        string timeString = time.ToString();
    //        return timeString.Replace(' ', '_').Replace(':', '_').Replace('/', '_');
    //    }
    //}
}
