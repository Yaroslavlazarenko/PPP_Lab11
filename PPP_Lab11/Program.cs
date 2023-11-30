using System.Text.Json;
using System;

namespace PPP_Lab11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Person> persons;

            FileStream file = new FileStream("persons.json", FileMode.Open);
            persons = JsonSerializer.Deserialize<IEnumerable<Person>>(file) ?? Enumerable.Empty<Person>();

            Console.WriteLine("Список персон после простой фильтрации (поиска):");
            IEnumerable<Person> sortedPersons = PersonFilters.SearchPersons(persons, "Mat");
            foreach (Person person in sortedPersons)
            {
                Console.WriteLine(person);
            }

            Console.WriteLine("\nСписок персон после составной фильтрации по нескольким полям:");
            IEnumerable<Person> filterPersons = PersonFilters.FilterPersons(persons, new {FirstName = "M", LastName = "R"});
            foreach (Person person in filterPersons)
            {
                Console.WriteLine(person);
            }

            float averageAge = PersonFilters.ProcessData(persons, p => p.Age, "average");
            Console.WriteLine($"\nСредний возраст: {averageAge}");

            float minWeight = PersonFilters.ProcessData(persons, p => p.Weight, "min");
            Console.WriteLine($"\nМинимальный вес (кг): {minWeight}");

            float maxHairLength = PersonFilters.ProcessData(persons, p => p.HairLength, "max");
            Console.WriteLine($"\nМаксимальная длина волос (см): {maxHairLength}");

        }
    }
}