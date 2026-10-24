using System;
using System.Collections.Generic;
using System.Linq;

namespace BabushkaHospital
{
    public class Disease
    {
        public string Name { get; set; }
        public Disease(string name) => Name = name;
        public override string ToString() => Name;
    }

    public class Medicine
    {
        public string Name { get; set; }
        public Disease TargetDisease { get; set; }
        public Medicine(string name, Disease disease)
        {
            Name = name;
            TargetDisease = disease;
        }
        public override string ToString() => $"{Name} (от {TargetDisease.Name})";
    }

    public class Babushka
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Disease> Diseases { get; set; } = new List<Disease>();
        public List<Medicine> Medicines { get; set; } = new List<Medicine>();

        public Babushka(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public void AddDisease(Disease d, Medicine m)
        {
            Diseases.Add(d);
            Medicines.Add(m);
        }

        public override string ToString()
        {
            string dis = Diseases.Count == 0 ? "нет болезней" : string.Join(", ", Diseases);
            string med = Medicines.Count == 0 ? "нет лекарств" : string.Join(", ", Medicines);
            return $"{Name} ({Age} лет) | Болезни: {dis} | Лекарства: {med}";
        }
    }

    public class Hospital
    {
        public string Name { get; set; }
        public List<Disease> TreatedDiseases { get; set; } = new List<Disease>();
        public int Capacity { get; set; }
        public Stack<Babushka> Patients { get; set; } = new Stack<Babushka>();

        public Hospital(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
        }

        public bool IsFull => Patients.Count >= Capacity;
        public int FreePlaces => Capacity - Patients.Count;
        public double FillPercent => Capacity == 0 ? 0 : (double)Patients.Count / Capacity * 100;

        public bool CanTreat(Babushka b)
        {
            if (b.Diseases.Count == 0) return true;
            int matched = b.Diseases.Count(d => TreatedDiseases.Any(t => t.Name == d.Name));
            return matched * 2 > b.Diseases.Count;
        }

        public override string ToString()
        {
            string dis = TreatedDiseases.Count == 0 ? "—" : string.Join(", ", TreatedDiseases);
            return $"{Name} | Лечит: {dis} | Пациентов: {Patients.Count}/{Capacity} | " +
                   $"Заполненность: {FillPercent:F1}%";
        }
    }

    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Disease gripp = new Disease("Грипп");
            Disease prostud = new Disease("Простуда");
            Disease davlenie = new Disease("Давление");
            Disease serdce = new Disease("Сердце");
            Disease sustavy = new Disease("Суставы");
            Disease[] allDiseases = { gripp, prostud, davlenie, serdce, sustavy };

            Stack<Hospital> hospitals = new Stack<Hospital>();

            hospitals.Push(new Hospital("Городская больница №1", 3)
            { TreatedDiseases = { gripp, prostud, davlenie } });
            hospitals.Push(new Hospital("Кардиоцентр", 2)
            { TreatedDiseases = { serdce, davlenie } });
            hospitals.Push(new Hospital("Ревматология", 2)
            { TreatedDiseases = { sustavy, prostud } });

            Queue<Babushka> babushki = new Queue<Babushka>();

            Console.WriteLine("Введите количество бабуль:");
            int n = int.Parse(Console.ReadLine() ?? "0");

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\n--- Бабуля #{i + 1} ---");
                Console.Write("Имя: ");
                string name = Console.ReadLine() ?? "Безымянная";
                Console.Write("Возраст: ");
                int age = int.Parse(Console.ReadLine() ?? "0");

                Babushka b = new Babushka(name, age);

                Console.Write("Сколько болезней? ");
                int cnt = int.Parse(Console.ReadLine() ?? "0");

                for (int j = 0; j < cnt; j++)
                {
                    Console.WriteLine("Доступные болезни: " + string.Join(", ", allDiseases.Select(x => x.Name)));
                    Console.Write("Болезнь: ");
                    string dName = (Console.ReadLine() ?? "").Trim();
                    Disease d = allDiseases.FirstOrDefault(x =>
                        x.Name.Equals(dName, StringComparison.OrdinalIgnoreCase));
                    if (d == null) { Console.WriteLine("Неизвестная болезнь."); continue; }

                    Console.Write($"Лекарство от {d.Name}: ");
                    string mName = Console.ReadLine() ?? "Таблетка";
                    b.AddDisease(d, new Medicine(mName, d));
                }

                babushki.Enqueue(b);  // бабуля встаёт в очередь
            }


            Hospital[] hospitalsOrder = hospitals.Reverse().ToArray();

            Queue<Babushka> street = new Queue<Babushka>();

            Console.WriteLine("\n===== РАСПРЕДЕЛЕНИЕ =====\n");

            while (babushki.Count > 0)
            {
                Babushka b = babushki.Dequeue();
                Hospital target = null;

                if (b.Diseases.Count == 0)
                {
                    // "только спросить" — первая свободная клиника
                    target = hospitalsOrder.FirstOrDefault(h => !h.IsFull);
                    Console.WriteLine(target != null
                        ? $"{b.Name} (без болезней) → в {target.Name} (спросить)"
                        : $"{b.Name} (без болезней) не нашла свободной клиники.");
                }
                else
                {
                    target = hospitalsOrder.FirstOrDefault(h => !h.IsFull && h.CanTreat(b));
                    Console.WriteLine(target != null
                        ? $"{b.Name} → в {target.Name}"
                        : $"{b.Name} — ни одна больница не лечит (>50% болезней) или нет мест.");
                }

                if (target != null)
                    target.Patients.Push(b);  
                else
                    street.Enqueue(b);         // плакать на улице
            }

            Console.WriteLine("\n===== СПИСОК ВСЕХ БАБУЛЬ ПО БОЛЬНИЦАМ =====");
            foreach (var h in hospitalsOrder)
            {
                if (h.Patients.Count > 0)
                {
                    Console.WriteLine($"\n[{h.Name}] (сверху стека — последняя поступившая):");
                    foreach (var b in h.Patients)
                        Console.WriteLine("  " + b);
                }
            }

            if (street.Count > 0)
            {
                Console.WriteLine("\n[На улице плачут]:");
                foreach (var b in street)
                    Console.WriteLine("  " + b);
            }

            Console.WriteLine("\n===== СПИСОК ВСЕХ БОЛЬНИЦ =====");
            foreach (var h in hospitalsOrder)
                Console.WriteLine(h);
        }
    }
}