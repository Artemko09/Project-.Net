using System;

namespace Project_course.Models
{
    public class Diya
    {
        public string Nazva { get; set; }
        public TimeSpan ChasPochatku { get; set; }
        public TimeSpan Tryvalist { get; set; }

        public override string ToString()
        {
            return $"{Nazva};{ChasPochatku:hh\\:mm};{Tryvalist:hh\\:mm}";
        }
    }
}
