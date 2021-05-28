using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace YetiFTPClient
{
    class Logging
    {
        /*
         * Format log file as:
         * 
         * Human readable explanation.. see full exception below:
         * Exception message
         */
        private static List<Logging> loggingObjects = new List<Logging>();
        private List<String> logs;
        private string ip;
        public Logging(string ip)
        {
            logs = new List<string>();
            this.ip = ip;
        }

        public void Log(string message)
        {
            logs.Add(message);
        }

        public static Logging GetLogging(string ip)
        {
            foreach (Logging logging in loggingObjects)
                if (logging.ip == ip)
                    return logging;
            return null;
        }

        public static void TryLog(string ip, string message)
        {
            foreach(Logging logging in loggingObjects.ToArray())
                if(logging.ip == ip)
                {
                    logging.logs.Add(message);
                    return;
                }

            Logging logger = new Logging(ip);
            logger.Log(message);
            loggingObjects.Add(logger);
        }

        public static void SaveLogs()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string filePath = $"{folderPath}\\SmartTransfer_Scan_Log_{DateTime.Now:dd_mm_yyyy-hh-mm}.txt";

            using(StreamWriter writer = File.CreateText(filePath))
            {
                foreach(Logging logging in loggingObjects)
                {
                    writer.WriteLine("------------------------------------");
                    writer.WriteLine("IP: " + logging.ip);
                    writer.WriteLine();
                    foreach (string log in logging.logs)
                    {
                        writer.WriteLine(log);
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}
