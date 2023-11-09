/*
 * 
 * Dziedziczenie – zadanie 1
•	Napisz program w języku C#, który ilustruje pojęcia programowania obiektowego, takie jak klasy, dziedziczenie, właściwości i metody.
•	Zdefiniuj klasę bazową Person, która ma pola name, surname i dateOfBirth oraz konstruktor przyjmujący te wartości jako parametry.
•	Dodaj do klasy Person metodę GetFullName, która zwraca pełne imię i nazwisko osoby, oraz właściwość Age, która oblicza wiek osoby na podstawie daty urodzenia.
•	Zdefiniuj klasę Address, która ma pola city, street, houseNumber i postalCode jako właściwości oraz konstruktor przyjmujący te wartości jako parametry.
•	Dodaj do klasy Person pole address typu Address i zmodyfikuj konstruktor klasy Person, aby przyjmował obiekt klasy Address jako parametr.
•	Zdefiniuj klasę pochodną Student, która dziedziczy po klasie Person i ma dodatkowe pole studentNumber oraz konstruktor przyjmujący te wartości jako parametry.
•	Zdefiniuj klasę pochodną Teacher, która dziedziczy po klasie Person i ma dodatkowe pole subjects typu List<string> oraz konstruktor przyjmujący te wartości jako parametry.
•	Utwórz obiekty każdej klasy, używając słowa kluczowego new i podając odpowiednie wartości w konstruktorach.
•	Wyświetl dane utworzonych obiektów, używając metody Console.WriteLine i właściwości obiektów. 
 * 
 */

using System.Reflection;

namespace programowanie_obiektowe3
{
    class Person
    {
        public string Name { get; }
        public string Surname { get; }
        public DateTime DateOfBirth { get; }
        public string GetFullName()
        {
            return Name + " " + Surname;
        }

        public int Age
        {
            get
            {
                return (int)((DateTime.Now - DateOfBirth).TotalDays / 365.25);

            }
        }
        public Address Address { get; set; }
        public Person(string name, string surname, DateTime dateOfBirth, Address address)
        {
            DateOfBirth = dateOfBirth;
            Name = name;
            Surname = surname;
            Address = address;
        }
    }

    class Address
    {
        public string City { get; }
        public string Street { get; }
        public string HouseNumber { get; }
        public string PostalCode { get; }
        public Address(string city, string street, string houseNumber, string postalCode)
        {
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            PostalCode = postalCode;
        }
    }

    class Student : Person
    {
        public int StudentNumber { get; }
        public Student(string name, string surname, DateTime dateOfBirth, Address address, int studentNumber) : base(name, surname, dateOfBirth, address)
        {
            StudentNumber = studentNumber;
        }
    }

    class Teacher : Person
    {
        public List<string> Subjects { get; }
        public Teacher(string name, string surname, DateTime dateOfBirth, Address address, List<string> subjects) : base(name, surname, dateOfBirth, address)
        {
            Subjects = subjects;
        }
    }

    internal class Program
    {
        static void Main()
        {
            Address adres_osoba = new("Poznań", "Długa", "1", "61-000");
            Console.WriteLine("Address adres_osoba:");
            foreach (PropertyInfo propertyInfo in typeof(Address).GetProperties())
            {
                Console.WriteLine(propertyInfo.Name + " => " + propertyInfo.GetValue(adres_osoba));
            }

            Console.WriteLine();

            Person osoba = new("Jan", "Kowalski", new DateTime(1999, 12, 12), adres_osoba);
            Console.WriteLine("Person osoba:");
            foreach (PropertyInfo propertyInfo in typeof(Person).GetProperties())
            {
                if (propertyInfo.Name != "Address")
                    Console.WriteLine(propertyInfo.Name + " => " + propertyInfo.GetValue(osoba));
                else
                {
                    foreach (PropertyInfo propertyInfoAddress in typeof(Address).GetProperties())
                    {
                        Console.WriteLine(propertyInfo.Name + "." + propertyInfoAddress.Name + " => " + propertyInfoAddress.GetValue(osoba.Address));
                    }
                }
            }

            Console.WriteLine();
            
            Address adres_student = new("Poznań", "Długa", "5", "61-001");
            Student student = new("Jan", "Kowalski", new DateTime(1991, 3, 25), adres_student, 12345);
            Console.WriteLine("Student student:");
            foreach(PropertyInfo propertyInfo in typeof(Student).GetProperties())
            {
                if (propertyInfo.Name != "Address")
                    Console.WriteLine(propertyInfo.Name + " => " + propertyInfo.GetValue(student));
                else
                {
                    foreach (PropertyInfo propertyInfoAddress in typeof(Address).GetProperties())
                    {
                        Console.WriteLine(propertyInfo.Name + "." + propertyInfoAddress.Name + " => " + propertyInfoAddress.GetValue(student.Address));
                    }
                }
            }
            
            Console.WriteLine();

            Address address_teacher = new("Poznań", "Długa", "10", "61-002");
            List<string> subjects = new() { "Matematyka", "Fizyka" };
            Teacher teacher = new("Jan", "Kowalski", new DateTime(2001, 1, 14), address_teacher, subjects);
            Console.WriteLine("Teacher teacher:");
            foreach (PropertyInfo propertyInfo in typeof(Teacher).GetProperties())
            {
                if (propertyInfo.Name == "Address")
                    foreach (PropertyInfo propertyInfoAddress in typeof(Address).GetProperties())
                    {
                        Console.WriteLine(propertyInfo.Name + "." + propertyInfoAddress.Name + " => " + propertyInfoAddress.GetValue(teacher.Address));
                    }
                else if (propertyInfo.Name == "Subjects")
                    foreach (string subject in teacher.Subjects)
                    {
                        Console.WriteLine(propertyInfo.Name + " => " + subject);
                    }
                else
                {
                    Console.WriteLine(propertyInfo.Name + " => " + propertyInfo.GetValue(teacher));
                }
            }

        }
    }
}