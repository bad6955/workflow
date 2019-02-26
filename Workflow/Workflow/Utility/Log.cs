using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workflow.Utility
{
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

        public static void Info(String info)
        {
            instance.logger.Information(info);
        }
        
        public static void Warn(String warning)
        {
            instance.logger.Warning(warning);
        }

        public static void Debug(String debug)
        {
            instance.logger.Debug(debug);
        }
    
        public static void Error(String error)
        {
            instance.logger.Error(error);
        }
    }
}