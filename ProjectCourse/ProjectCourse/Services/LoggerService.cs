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
                // Шлях до конфігураційного файлу log4net
                var configFile = new FileInfo("log4net.config");
                if (!configFile.Exists)
                {
                    Console.WriteLine("Не знайдено log4net.config! Логи не будуть записані.");
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
                Console.WriteLine("Помилка ініціалізації логера: " + ex.Message);
            }
        }

        public static void Log(Exception ex)
        {
            if (log != null)
                log.Error("Виникла помилка", ex);
            else
                Console.WriteLine("Логер не ініціалізований: " + ex.Message);
        }

        public static void LogMessage(string message)
        {
            if (log != null)
                log.Info(message);
            else
                Console.WriteLine("Логер не ініціалізований: " + message);
        }
    }
}
