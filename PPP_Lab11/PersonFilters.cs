using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPP_Lab11
{
    public class PersonFilters
    {
        /// <summary>
        /// Выполняет поиск персон в коллекции по заданному поисковому запросу.
        /// Поиск осуществляется по имени, фамилии, возрасту, весу и длине волос персон.
        /// </summary>
        /// <param name="persons">Коллекция персон, в которой выполняется поиск.</param>
        /// <param name="searchTerm">Строка поискового запроса.</param>
        /// <returns>Коллекция персон, удовлетворяющих поисковому запросу.</returns>
        /// <exception cref="ArgumentNullException">Генерируется, если переданная коллекция персон равна null.</exception>
        /// <exception cref="ArgumentException">Генерируется, если поисковый запрос является пустым или null.</exception>
        public static IEnumerable<Person> SearchPersons(IEnumerable<Person> persons, string searchTerm)
        {
            if (persons == null)
                throw new ArgumentNullException("Коллекция персон не может быть null");

            if (string.IsNullOrEmpty(searchTerm))
                throw new ArgumentException("Поисковый запрос не может быть пустым или null.");

            return persons.Where(p =>
                p.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Age.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Weight.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.HairLength.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            );
        }

        /// <summary>
        /// Выполняет фильтрацию коллекции персон на основе заданных критериев фильтрации.
        /// Фильтрация осуществляется по свойствам объекта filterCriteria, где не null значения используются в качестве фильтров.
        /// </summary>
        /// <param name="persons">Коллекция персон для фильтрации.</param>
        /// <param name="filterCriteria">Объект, содержащий критерии фильтрации.</param>
        /// <returns>Отфильтрованная коллекция персон, удовлетворяющих критериям фильтрации.</returns>
        /// <exception cref="ArgumentNullException">Генерируется, если переданная коллекция персон или критерии фильтрации равны null.</exception>
        public static IEnumerable<Person> FilterPersons(IEnumerable<Person> persons, object filterCriteria)
        {
            if (persons == null)
                throw new ArgumentNullException(nameof(persons), "Коллекция персон не может быть null.");

            if (filterCriteria == null)
                throw new ArgumentNullException(nameof(filterCriteria), "Критерии фильтрации не могут быть null.");

            var properties = filterCriteria.GetType().GetProperties()
                                .Where(p => p.GetValue(filterCriteria) != null);

            var filteredPersons = persons.AsQueryable();

            foreach (var property in properties)
            {
                var propValue = property.GetValue(filterCriteria);

                filteredPersons = filteredPersons
                    .Where(p => p.GetType()
                    .GetProperty(property.Name)
                    .GetValue(p)
                    .ToString()
                    .Contains(propValue.ToString(), StringComparison.OrdinalIgnoreCase));
            }

            return filteredPersons.ToList();
        }

        /// <summary>
        /// Выполняет статистическую обработку данных персон по заданному критерию.
        /// Критерий может быть 'min' (минимальное значение), 'max' (максимальное значение) или 'average' (среднее значение).
        /// </summary>
        /// <param name="persons">Коллекция персон для статистической обработки.</param>
        /// <param name="selector">Функция, выбирающая числовое значение из объекта персоны для статистической обработки.</param>
        /// <param name="criteria">Критерий для определения минимального, максимального или среднего значения.</param>
        /// <returns>Результат статистической обработки данных в коллекции персон.</returns>
        /// <exception cref="ArgumentNullException">Генерируется, если переданная коллекция персон или селектор равны null.</exception>
        /// <exception cref="ArgumentException">Генерируется, если критерий не соответствует 'min', 'max' или 'average'.</exception>
        public static float ProcessData(IEnumerable<Person> persons, Func<Person, float> selector, string criteria)
        {
            if (persons == null)
                throw new ArgumentNullException("Коллекция персон не может быть null.");

            if (selector == null)
                throw new ArgumentNullException("Selector не может быть null.");

            if (string.IsNullOrEmpty(criteria))
                throw new ArgumentException("Критерия поиска не может быть null или пустым.");

            switch (criteria.ToLower())
            {
                case "min":
                    return persons.Min(selector);
                case "max":
                    return persons.Max(selector);
                case "average":
                    return persons.Average(selector);
                default:
                    throw new ArgumentException("Непракильная критерия. Испльзуйте 'min', 'max', или 'average'.");
            }
        }


    }
}
