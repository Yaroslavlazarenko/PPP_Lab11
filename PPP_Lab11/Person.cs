using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PPP_Lab11
{
    public class Person
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private float _age;
        private float _weight;
        private float _hairLength;

        private static readonly float MAX_AGE = 122;
        private static readonly float MAX_WEIGHT = 1140;
        private static readonly float MAX_HAIR_LENGTH = 1560;

        [JsonPropertyName("id")]
        public int Id 
        { 
            get => _id; 
            set => _id = value > 0 ? value : throw new ArgumentOutOfRangeException("Id пользователя не может быть больше 0"); 
        }

        [JsonPropertyName("first_name")]
        public string FirstName 
        { 
            get => _firstName; 
            set => _firstName = !string.IsNullOrEmpty(value) ? value : throw new ArgumentNullException("First name пользователя не может быть null"); 
        }

        [JsonPropertyName("last_name")]
        public string LastName 
        { 
            get => _lastName; 
            set => _lastName = !string.IsNullOrEmpty(value) ? value : throw new ArgumentNullException("Last name пользователя не может быть null"); 
        }

        [JsonPropertyName("age")]
        public float Age 
        { 
            get => _age; 
            set => _age = value > 0 && value < MAX_AGE ? value : throw new ArgumentOutOfRangeException($"Возраст персоны должен быть больше нуля и меньше {MAX_AGE}"); 
        }

        [JsonPropertyName("weight")]
        public float Weight 
        {
            get => _weight;
            set => _weight = value > 0 && value < MAX_WEIGHT ? value : throw new ArgumentOutOfRangeException($"Вес персоны должен быть больше нуля и меньше {MAX_WEIGHT}");
        }

        [JsonPropertyName("hair_length")]

        public float HairLength 
        {
            get => _hairLength;
            set => _hairLength = value >= 0 && value < MAX_HAIR_LENGTH ? value : throw new ArgumentOutOfRangeException($"Длина волос персоны должна быть не меньше нуля и меньше {MAX_HAIR_LENGTH}");
        }

        public Person(int id, string firstName, string lastName, float age, float weight, float hairLength)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Weight = weight;
            HairLength = hairLength;
        }

        public override string ToString()
        {
            return $"Id: {Id,-3}\tFirst Name: {FirstName,-10}\tLast Name: {LastName,-10}\tAge: {Age,-3}\tWeight: {Weight, -5}\tHair length: {HairLength, -6}";
        }
    }
}
