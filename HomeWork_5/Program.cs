using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentsApp
{
    public class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int BirthYear { get; set; }
        public string ExamSubject { get; set; }
        public int Score { get; set; }

        public Student() { }

        public Student(string lastName, string firstName, int birthYear, string examSubject, int score)
        {
            LastName = lastName;
            FirstName = firstName;
            BirthYear = birthYear;
            ExamSubject = examSubject;
            Score = score;
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} | Год рождения: {BirthYear} | Экзамен: {ExamSubject} | Баллы: {Score}";
        }

        public string ToFileString()
            => $"{LastName};{FirstName};{BirthYear};{ExamSubject};{Score}";

        public static Student FromFileString(string line)
        {
            var parts = line.Split(';');
            if (parts.Length != 5) return null;
            return new Student(parts[0].Trim(), parts[1].Trim(),
                int.Parse(parts[2]), parts[3].Trim(), int.Parse(parts[4]));
        }
    }

    class Program
    {
        private const string FilePath = "students.txt";

        static Dictionary<(string LastName, string FirstName), Student> students
            = new Dictionary<(string, string), Student>();

        static void Main(string[] args)
        {
            LoadStudents();

            if (students.Count == 0)
            {
                AddDefaultStudents();
                SaveStudents();
            }

            while (true)
            {
                Console.WriteLine("\n=== МЕНЮ ===");
                Console.WriteLine("1. Новый студент");
                Console.WriteLine("2. Удалить");
                Console.WriteLine("3. Сортировать");
                Console.WriteLine("4. Показать всех");
                Console.WriteLine("0. Выход");
                Console.Write("Введите команду: ");
                var cmd = Console.ReadLine()?.Trim().ToLower();

                switch (cmd)
                {
                    case "1":
                    case "новый студент":
                        AddStudent();
                        SaveStudents();
                        break;

                    case "2":
                    case "удалить":
                        RemoveStudent();
                        SaveStudents();
                        break;

                    case "3":
                    case "сортировать":
                        PrintAll(students.Values.OrderBy(s => s.Score));
                        break;

                    case "4":
                    case "показать":
                        PrintAll(students.Values);
                        break;

                    case "0":
                    case "выход":
                        return;

                    default:
                        Console.WriteLine("Неизвестная команда.");
                        break;
                }
            }
        }

        static void AddDefaultStudents()
        {
            var list = new List<Student>
            {
                new Student("Иванов", "Иван", 2008, "Математика", 85),
                new Student("Петров", "Пётр", 2008, "Физика", 78),
                new Student("Сидоров", "Сидор", 2009, "Информатика", 92),
                new Student("Кузнецова", "Анна", 2008, "Математика", 74),
                new Student("Смирнов", "Алексей", 2008, "Химия", 88),
                new Student("Волкова", "Мария", 2009, "Биология", 90),
                new Student("Морозов", "Дмитрий", 2009, "История", 81),
                new Student("Новикова", "Ольга", 2007, "Литература", 79),
                new Student("Фёдоров", "Николай", 2008, "Информатика", 95),
                new Student("Соколова", "Екатерина", 2008, "География", 83)
            };

            foreach (var s in list)
                students[(s.LastName, s.FirstName)] = s;
        }

        static void LoadStudents()
        {
            if (!File.Exists(FilePath)) return;

            foreach (var line in File.ReadAllLines(FilePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var s = Student.FromFileString(line);
                if (s != null)
                    students[(s.LastName, s.FirstName)] = s;
            }
        }

        static void SaveStudents()
        {
            File.WriteAllLines(FilePath, students.Values.Select(s => s.ToFileString()));
        }

        static void PrintAll(IEnumerable<Student> source)
        {
            int i = 1;
            bool any = false;
            foreach (var s in source)
            {
                Console.WriteLine($"{i++}. {s}");
                any = true;
            }
            if (!any) Console.WriteLine("Список пуст.");
        }

        static void AddStudent()
        {
            try
            {
                Console.Write("Фамилия: ");
                var lastName = Console.ReadLine()?.Trim();
                Console.Write("Имя: ");
                var firstName = Console.ReadLine()?.Trim();
                Console.Write("Год рождения: ");
                var birthYear = int.Parse(Console.ReadLine()!);
                Console.Write("Экзамен: ");
                var exam = Console.ReadLine()?.Trim();
                Console.Write("Баллы: ");
                var score = int.Parse(Console.ReadLine()!);

                var key = (lastName, firstName);
                if (students.ContainsKey(key))
                {
                    Console.WriteLine("Студент с такой фамилией и именем уже существует.");
                    return;
                }

                students[key] = new Student(lastName, firstName, birthYear, exam, score);
                Console.WriteLine("Студент добавлен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка ввода: {ex.Message}");
            }
        }

        static void RemoveStudent()
        {
            Console.Write("Введите фамилию: ");
            var lastName = Console.ReadLine()?.Trim();
            Console.Write("Введите имя: ");
            var firstName = Console.ReadLine()?.Trim();

            var key = (lastName, firstName);
            if (students.Remove(key))
                Console.WriteLine($"Студент {lastName} {firstName} удалён.");
            else
                Console.WriteLine("Студент не найден.");
        }
    }
}