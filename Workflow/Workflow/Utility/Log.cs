using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workflow.Utility
{
    /*
    public class Log
    {
        private static Log instance;
        private Serilog.Core.Logger logger;

        public static void Init()
        {
            if (instance != null)
            {
                return;
            }
            else
            {
                instance = new Log();
            }
        }

        public static void Shutdown()
        {
            instance.logger.Dispose();
        }

        private Log()
        {
            logger = new LoggerConfiguration().WriteTo.File("log.txt").CreateLogger();
        }

        public static void Info(string info)
        {
            instance.logger.Information(info);
        }
        
        public static void Warn(string warning)
        {
            instance.logger.Warning(warning);
        }

        public static void Debug(string debug)
        {
            instance.logger.Debug(debug);
        }
    
        public static void Error(string error)
        {
            instance.logger.Error(error);
        }
    }
    */

    public static class Log
    {
        public static void Info(string info)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("./log.txt", true))
            {
                DateTime now = DateTime.Now;
                string line = String.Format("[{0} : {1}] {2}", now.ToShortDateString(), now.ToShortTimeString(), info);
                file.WriteLine(line);
            }
        }

        public static List<String> ReadLog()
        {
            List<string> logLines = new List<string>();
            using (System.IO.StreamReader file = new System.IO.StreamReader("./log.txt"))
            {
                DateTime now = DateTime.Now;
                string line;
                while((line = file.ReadLine()) != null)
                {
                    logLines.Add(line);
                }
            }
            return logLines;
        }
    }
}