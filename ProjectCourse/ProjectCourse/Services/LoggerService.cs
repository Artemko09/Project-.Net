using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Reflection;

namespace Project_course.Services
{
    public static class LoggerService
    {
        private static readonly ILog log;

        static LoggerService()
        {
            try
            {
                var configFile = new FileInfo("log4net.config");
                if (!configFile.Exists)
                {
                    Console.WriteLine("Íå çíàéäåíî log4net.config! Ëîãè íå áóäóòü çàïèñàí³.");
                }
                else
                {
                    var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
                    XmlConfigurator.Configure(logRepository, configFile);
                }

                log = LogManager.GetLogger(typeof(LoggerService));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ïîìèëêà ³í³ö³àë³çàö³¿ ëîãåðà: " + ex.Message);
            }
        }

        public static void Log(Exception ex)
        {
            if (log != null)
                log.Error("Âèíèêëà ïîìèëêà", ex);
            else
                Console.WriteLine("Ëîãåð íå ³í³ö³àë³çîâàíèé: " + ex.Message);
        }

        public static void LogMessage(string message)
        {
            if (log != null)
                log.Info(message);
            else
                Console.WriteLine("Ëîãåð íå ³í³ö³àë³çîâàíèé: " + message);
        }
    }
}

