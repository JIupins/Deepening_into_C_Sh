using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace S2_HW1
{
    internal class Person
    {
        public enum Gender
        {
            M,
            W
        }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string GenderP { get; set; }
        public int Age { get; set; }
        public bool InFamily { get; set; } = false;
        public int CountOfElements { get; set; }

        public Person(string lastName, string firstName, Gender gender, int age)
        {
            LastName = lastName;
            FirstName = firstName;
            Age = age;

            CountOfElements = 5;

            if (gender == Gender.M) this.GenderP = "М";
            else this.GenderP = "Ж";
        }
    }
}
