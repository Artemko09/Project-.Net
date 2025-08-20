using Project_course.Models;
using System;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace Project_course.Services
{
    public static class FileService
    {
        public static RozporyadokDnya LoadFromTxt(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var schedule = new RozporyadokDnya
            {
                Data = DateTime.Parse(lines[0])
            };

            for (int i = 1; i < lines.Length; i++)
            {
                var parts = lines[i].Split(';');
                schedule.DiyList.Add(new Diya
                {
                    Nazva = parts[0],
                    ChasPochatku = TimeSpan.Parse(parts[1]),
                    Tryvalist = TimeSpan.Parse(parts[2])
                });
            }

            return schedule;
        }

        public static void SaveToTxt(string filePath, RozporyadokDnya schedule)
        {
            using var writer = new StreamWriter(filePath);
            writer.WriteLine(schedule.Data.ToShortDateString());
            foreach (var d in schedule.DiyList)
            {
                writer.WriteLine(d.ToString());
            }
        }

        public static void SaveToJson(string filePath, RozporyadokDnya schedule)
        {
            var json = JsonSerializer.Serialize(schedule, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public static void SaveToXml(string filePath, RozporyadokDnya schedule)
        {
            var serializer = new XmlSerializer(typeof(RozporyadokDnya));
            using var fs = new FileStream(filePath, FileMode.Create);
            serializer.Serialize(fs, schedule);
        }
    }
}
