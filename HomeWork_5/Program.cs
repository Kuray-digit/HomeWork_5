using System;
using System.Collections.Generic; 

namespace Homework
{
    class Program
    {
        static double[] CalculateMonthlyAverages(Dictionary<string, int[]> temperature)
        {
            double[] averages = new double[temperature.Count];
            int index = 0;

            foreach (var month in temperature)
            {
                int sum = 0;
                int days = month.Value.Length; 
                for (int j = 0; j < days; j++)
                {
                    sum += month.Value[j];
                }

                averages[index] = Math.Round((double)sum / days);
                index++;
            }

            return averages;
        }

        public static void Main(string[] args)
        {
            string[] monthNames =
            {
                "Январь", "Февраль", "Март", "Апрель",
                "Май", "Июнь", "Июль", "Август",
                "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
            };

            const int Days = 30;
            Dictionary<string, int[]> temperature = new Dictionary<string, int[]>();

            Random rnd = new Random();

            foreach (string month in monthNames)
            {
                int[] monthTemps = new int[Days];
                for (int j = 0; j < Days; j++)
                {
                    monthTemps[j] = rnd.Next(-20, 31);
                }
                temperature.Add(month, monthTemps);
            }

            double[] averageTemps = CalculateMonthlyAverages(temperature);

            Console.WriteLine("Средняя температура по месяцам:");

            int count = 0;
            foreach (var month in temperature)
            {
                Console.WriteLine($"{month.Key,-10}: {averageTemps[count]} °C");
                count++;
            }

            Array.Sort(averageTemps);

            Console.WriteLine("\nСредние температуры, отсортированные по возрастанию:");
            for (int i = 0; i < averageTemps.Length; i++)
            {
                Console.WriteLine($"{averageTemps[i]} °C");
            }
        }
    }
}