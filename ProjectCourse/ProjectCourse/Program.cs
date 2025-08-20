using System;
using Project_course.Models;
using Project_course.Services;

namespace Project_course
{
    class Program
    {
        static void Main()
        {
            try
            {
                var schedule = FileService.LoadFromTxt("rozporyadok.txt");

                var totalWalk = schedule.ZagTryvalistProgulyanok();
                Console.WriteLine($"Загальна тривалість прогулянок: {totalWalk}");

                if (totalWalk.TotalMinutes < 120)
                {
                    schedule.ZaminaTVnaProgulyankuDo2God();
                }

                FileService.SaveToTxt("Файл1.txt", schedule);
                FileService.SaveToJson("rozporyadok.json", schedule);
                FileService.SaveToXml("rozporyadok.xml", schedule);
            }
            catch (Exception ex)
            {
                LoggerService.Log(ex);
                Console.WriteLine("Сталася помилка! Деталі у лог-файлі.");
            }

            Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
            Console.ReadKey();

        }
    }
}
