using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;
using static S2_HW1.Person;

namespace S2_HW1
{
    internal class Program
    {
        static List<Person> People = new List<Person>();
        static List<Family> Familys = new List<Family>();

        static Dictionary<int, Family> FamilysDict = new Dictionary<int, Family>();
        static int CountOfFamilys = 0;

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Создать человека");
                Console.WriteLine("2. Создать семью");
                Console.WriteLine("3. Добавить человека в семью");
                Console.WriteLine("4. Удалить человека");
                Console.WriteLine("5. Удалить семью");
                Console.WriteLine("6. Вывести список всех людей (таблицей)");
                Console.WriteLine("7. Вывести список всех семей (таблицей)");
                Console.WriteLine("8. Выход\n");

                switch (Console.ReadLine())
                {
                    case "1":
                        CreatePerson();
                        break;
                    case "2":
                        CreateFamily();
                        break;
                    case "3":
                        //AddPersonToFamily();
                        break;
                    case "4":
                        DeletePerson();
                        break;
                    case "5":
                        DeleteFamily();
                        break;
                    case "6":
                        PrintArrayAsTableForPeople(People);
                        break;
                    case "7":
                        PrintArrayAsTableForFamily(Familys);
                        break;
                    case "8":
                        return;
                    default:
                        Console.WriteLine("Некорректный ввод. Пожалуйста, выберите пункт меню.\n");
                        break;
                }
            }
        }

        static void CreatePerson()
        {
            Console.Write("Введите фамилию: ");
            string lastName = Console.ReadLine();

            Console.Write("Введите имя: ");
            string firstName = Console.ReadLine();

            Console.Write("Введите пол (м/ж): ");
            string genderInput = Console.ReadLine();
            Gender gender;
            if (genderInput.ToLower() == "м")
            {
                gender = Gender.M;
            }
            else if (genderInput.ToLower() == "ж")
            {
                gender = Gender.W;
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, повторите ввод.");
                CreatePerson();
                return;
            }

            Console.Write("Введите возраст: ");
            string ageInput = Console.ReadLine();
            if (!int.TryParse(ageInput, out int age))
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, повторите ввод.");
                CreatePerson();
                return;
            }

            People.Add(new Person(lastName, firstName, gender, age));

            Console.WriteLine("Человек создан.\n");
        }

        static void CreateFamily()
        {
            Family newFamily;

            Person father = DefinePersonForFamily("Отца", People);
            Person mother = DefinePersonForFamily("Матери", People);
            Person[] children;

            while (true)
            {
                Console.Write("Хотите добавить детей в семью (Д/Н)?: ");
                string childExist = Console.ReadLine();

                if (childExist.ToLower() == "д")
                {
                    Console.Write("Введите колличество детей в семье: ");
                    string childCoun = Console.ReadLine();

                    if (int.TryParse(childCoun, out int count))
                    {
                        children = new Person[count];

                        for (int i = 0; i < count; i++)
                        {
                            children[i] = DefinePersonForFamily($"{i + 1}-го ребенка", People);
                        }

                        newFamily = new Family(father, mother, children);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Некорректный ввод. Пожалуйста, введите колличество цифрой!\n");
                    }
                }
                else if (childExist.ToLower() == "н")
                {
                    newFamily = new Family(father, mother);
                    break;
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите Д или Н!\n");
                }
            }

            Familys.Add(newFamily);

            CountOfFamilys++;
            FamilysDict.Add(CountOfFamilys, newFamily);

            Console.WriteLine("Семья создана.");
        }

        static Person DefinePersonForFamily(string note, List<Person> people)
        {
            Person? personForAdd = null;
            bool flag = true;

            while (flag)
            {

                Console.Write($"Введите фамилию {note}. Если {note} нет введите Н: ");
                string? lastNamePerson = Console.ReadLine();

                if (lastNamePerson.ToLower() == "н") return null;

                Console.Write($"Введите имя {note}: ");
                string? firstNamePerson = Console.ReadLine();

                foreach (var person in people)
                {
                    if (person.LastName.Equals(lastNamePerson) && person.FirstName.Equals(firstNamePerson))
                    {
                        personForAdd = person;
                        Console.Write($"Человек с именем: {firstNamePerson} и фамилией: {lastNamePerson} найден в базе!\n");
                        person.InFamily = true;
                        flag = false;
                        break;
                    }
                }

                if (personForAdd == null)
                {
                    Console.Write($"Человека с именем: {firstNamePerson} и фамилией: {lastNamePerson} не существует!\n");
                }
            }

            return personForAdd;
        }

        static void AddPersonToFamily()
        {
            Console.Write("Введите фамилию семьи: ");
            string lastNameFamily = Console.ReadLine();
            Family family = Familys.Find(f => f.LastNameFamily == lastNameFamily);
            if (family == null)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, повторите ввод.");
                AddPersonToFamily();
                return;
            }
            Console.Write("Введите имя человека: ");
            string personFirstName = Console.ReadLine();
            Person person = People.Find(p => p.FirstName == personFirstName);
            if (person == null)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, повторите ввод.");
                AddPersonToFamily();
                return;
            }
            family.Children.Add(person);
            Console.WriteLine("Человек добавлен в семью.");
        }

        static void DeletePerson()
        {
            Console.Write("Введите фамилию: ");
            string lastName = Console.ReadLine();
            Console.Write("Введите имя: ");
            string firstName = Console.ReadLine();
            Person person = People.Find(p => p.LastName == lastName && p.FirstName == firstName);
            if (person == null)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, повторите ввод.");
                DeletePerson();
                return;
            }
            People.Remove(person);
            foreach (var family in Familys)
            {
                if (family.People.Contains(person))
                {
                    family.People.Remove(person);
                }
            }
            Console.WriteLine("Человек удален.");
        }

        static void DeleteFamily()
        {
            Console.Write("Введите фамилию семью: ");
            string lastNameFamily = Console.ReadLine();
            Family family = Familys.Find(f => f.LastNameFamily == lastNameFamily);
            if (family == null)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, повторите ввод.");
                DeleteFamily();
                return;
            }

            Familys.Remove(family);

            foreach (var pers in family.People)
            {
                pers.InFamily = false;
            }

            Console.WriteLine("Семья удалена.");
        }

        public static void PrintArrayAsTableForPeople(List<Person> elements)
        {
            int countPersonData = 0;
            string[,] arrayPersonData;
            Person person;

            if (elements.Count.Equals(0) || elements is null)
            {
                Console.WriteLine("Ещё не создано ни одного человека!\n");
            }
            else
            {
                person = (Person)((Object)elements[0]);
                countPersonData = person.CountOfElements;
            }

            arrayPersonData = new string[elements.Count, countPersonData];
            int countI = 0;

            foreach (Person pers in elements)
            {
                int countJ = 0;

                arrayPersonData[countI, countJ++] = pers.LastName;
                arrayPersonData[countI, countJ++] = pers.FirstName;
                arrayPersonData[countI, countJ++] = pers.GenderP;
                arrayPersonData[countI, countJ++] = pers.Age.ToString();
                arrayPersonData[countI, countJ++] = pers.InFamily.ToString();

                countI++;
            }

            CreateAndShowTable(arrayPersonData);
        }

        public static void PrintArrayAsTableForFamily(List<Family> elements)
        {
            if (elements.Count.Equals(0) || elements is null)
            {
                Console.WriteLine("Ещё не создано ни одной семьи!\n");
                return;
            }

            foreach (Family? fami in elements)
            {
                PrintArrayAsTableForPeople(fami.People);
            }
        }

        public static void CreateAndShowTable(string[,] arrayPersonData)
        {
            int columnCount = arrayPersonData.GetLength(1);
            int lineCount = arrayPersonData.GetLength(0);

            string[] title = { "Фамилия", "Имя", "Пол", "Возраст", "Статус" };

            int maxLengthOfCell = 0;

            foreach (var item in title)
            {
                if (item is not null && item.Length > maxLengthOfCell)
                {
                    maxLengthOfCell = item.Length;
                }
            }

            foreach (var item in arrayPersonData)
            {
                if (item is not null && item.Length > maxLengthOfCell)
                {
                    maxLengthOfCell = item.Length;
                }
            }

            Console.WriteLine();
            Console.WriteLine(new string('=', ((maxLengthOfCell + 5) * columnCount)) + '=');

            Console.Write("| ");

            for (int i = 0; i < title.Length; i++)
            {
                Console.Write(title[i].PadRight(maxLengthOfCell + 2));
                Console.Write(" | ");
            }
            Console.WriteLine();

            Console.WriteLine(new string('=', ((maxLengthOfCell + 5) * columnCount)) + '=');

            for (int i = 0; i < arrayPersonData.GetLength(0); i++)
            {
                Console.Write("| ");

                for (int j = 0; j < arrayPersonData.GetLength(1); j++)
                {
                    Console.Write(arrayPersonData[i, j].PadRight(maxLengthOfCell + 2));
                    Console.Write(" | ");
                }

                Console.WriteLine();
                if (i < arrayPersonData.GetLength(0) - 1) Console.WriteLine(new string('-', ((maxLengthOfCell + 5) * columnCount)) + '-');
            }

            Console.WriteLine(new string('=', ((maxLengthOfCell + 5) * columnCount)) + '=');
            Console.WriteLine();
        }
    }
}