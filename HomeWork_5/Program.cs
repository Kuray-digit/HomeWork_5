using System;

namespace Homework
{
    class Program
    {
        static double[] CalculateMonthlyAverages(int[,] temperature)
        {
            int months = temperature.GetLength(0);
            int days = temperature.GetLength(1);
            double[] averages = new double[months];

            for (int i = 0; i < months; i++)
            {
                int sum = 0;
                for (int j = 0; j < days; j++)
                {
                    sum += temperature[i, j];
                }
                averages[i] = Math.Round((double)sum / days);
            }

            return averages;
        }
        public static void Main(string[] args)
        {
            const int Months = 12;
            const int Days = 30;
            int[,] temperature = new int[Months, Days];

            Random rnd = new Random();
            for (int i = 0; i < Months; i++)
            {
                for (int j = 0; j < Days; j++)
                {
                    temperature[i, j] = rnd.Next(-20, 31);
                }
            }
            double[] averageTemps = CalculateMonthlyAverages(temperature);
            string[] monthNames =
            {
            "Январь", "Февраль", "Март", "Апрель",
            "Май", "Июнь", "Июль", "Август",
            "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
            };

            Console.WriteLine("Средняя температура по месяцам:");
            for (int i = 0; i < Months; i++)
            {
                Console.WriteLine($"{monthNames[i],-10}: {averageTemps[i]} °C");
            }
            // "-10" - считает сколько всего в строчке символов и прибавляет пробелы до 10
            Array.Sort(averageTemps);

            Console.WriteLine("\nСредние температуры, отсортированные по возрастанию:");
            for (int i = 0; i < Months; i++)
            {
                Console.WriteLine($"{averageTemps[i]} °C");
            }

        }
    }
}