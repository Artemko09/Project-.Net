using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_course.Models
{
    public class RozporyadokDnya
    {
        public DateTime Data { get; set; }
        public List<Diya> DiyList { get; set; } = new List<Diya>();

        public TimeSpan ZagTryvalistProgulyanok()
        {
            return DiyList
                .Where(d => d.Nazva.ToLower().Contains("walk"))
                .Aggregate(TimeSpan.Zero, (sum, d) => sum + d.Tryvalist);
        }

        public void ZaminaTVnaProgulyankuDo2God()
        {
            var totalWalk = ZagTryvalistProgulyanok();

            // Якщо вже >= 2 годин, нічого робити не треба
            if (totalWalk.TotalMinutes >= 120)
                return;

            // Знаходимо перший TV після 12:00
            var tv = DiyList.FirstOrDefault(d =>
                d.Nazva.ToLower() == "tv" &&
                d.ChasPochatku >= new TimeSpan(12, 0, 0));

            if (tv != null)
            {
                // Скільки хвилин потрібно додати до 2 годин
                var neededMinutes = 120 - totalWalk.TotalMinutes;

                // Якщо TV тривалість менше або рівна neededMinutes, змінюємо його на прогулянку
                if (tv.Tryvalist.TotalMinutes <= neededMinutes)
                {
                    tv.Nazva = "Walk";
                    totalWalk += tv.Tryvalist;
                    neededMinutes -= tv.Tryvalist.TotalMinutes;
                }
                else
                {
                    // TV більший за neededMinutes → скорочуємо TV і додаємо прогулянку потрібної тривалості
                    var newWalk = new Diya
                    {
                        Nazva = "Walk",
                        ChasPochatku = tv.ChasPochatku + TimeSpan.FromMinutes(tv.Tryvalist.TotalMinutes - neededMinutes),
                        Tryvalist = TimeSpan.FromMinutes(neededMinutes)
                    };
                    DiyList.Add(newWalk);
                    totalWalk += newWalk.Tryvalist;
                }
            }

            // Якщо все ще не вистачає часу (немає TV або TV замало), додаємо нову прогулянку в кінець дня
            if (totalWalk.TotalMinutes < 120)
            {
                var newWalk = new Diya
                {
                    Nazva = "Walk",
                    ChasPochatku = DiyList.Max(d => d.ChasPochatku + d.Tryvalist), // після останньої дії
                    Tryvalist = TimeSpan.FromMinutes(120 - totalWalk.TotalMinutes)
                };
                DiyList.Add(newWalk);
            }
        }

    }
}
