using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2_HW1
{
    internal class Family
    {
        public string LastNameFamily { get; set; }
        public Person Father { get; set; }
        public Person Mother { get; set; }
        public List<Person> Children { get; set; }
        public int CountOfElements { get; set; }
        public List<Person> People { get; set; } = new List<Person>();

        public Family(Person father, Person mother, params Person[] children)
        {
            Father = father;
            Mother = mother;
            Children = new List<Person>(children);

            if (father is not null) People.Add(Father);
            if (mother is not null) People.Add(Mother);

            foreach (var child in Children)
            {
                People.Add(child);
            }

            CountOfElements = People.Count;

            foreach (var pep in People)
            {
                if (pep is not null)
                {
                    LastNameFamily = pep.LastName;
                }
            }
        }
    }
}