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
                // ���� �� ���������������� ����� log4net
                var configFile = new FileInfo("log4net.config");
                if (!configFile.Exists)
                {
                    Console.WriteLine("�� �������� log4net.config! ���� �� ������ �������.");
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
                Console.WriteLine("������� ����������� ������: " + ex.Message);
            }
        }

        public static void Log(Exception ex)
        {
            if (log != null)
                log.Error("������� �������", ex);
            else
                Console.WriteLine("����� �� �������������: " + ex.Message);
        }

        public static void LogMessage(string message)
        {
            if (log != null)
                log.Info(message);
            else
                Console.WriteLine("����� �� �������������: " + message);
        }
    }
}
