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

            if (totalWalk.TotalMinutes >= 120)
                return;

            var tv = DiyList.FirstOrDefault(d =>
                d.Nazva.ToLower() == "tv" &&
                d.ChasPochatku >= new TimeSpan(12, 0, 0));

            if (tv != null)
            {
                var neededMinutes = 120 - totalWalk.TotalMinutes;

                if (tv.Tryvalist.TotalMinutes <= neededMinutes)
                {
                    tv.Nazva = "Walk";
                    totalWalk += tv.Tryvalist;
                    neededMinutes -= tv.Tryvalist.TotalMinutes;
                }
                else
                {
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
            
            if (totalWalk.TotalMinutes < 120)
            {
                var newWalk = new Diya
                {
                    Nazva = "Walk",
                    ChasPochatku = DiyList.Max(d => d.ChasPochatku + d.Tryvalist), 
                    Tryvalist = TimeSpan.FromMinutes(120 - totalWalk.TotalMinutes)
                };
                DiyList.Add(newWalk);
            }
        }

    }
}

