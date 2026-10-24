using System;
using System.IO;
using System.Collections.Generic;

namespace Homework
{
    class Program
    {
        public static string HasErrors(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Файл не найден: {filePath}");
                    return "";
                }

                return File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
                return "";
            }
        }

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Пожалуйста, укажите путь к файлу в качестве аргумента.");
                return;
            }

            string filePath = args[0];
            string content = HasErrors(filePath);
            if (content == "") return;

            string lowerText = content.ToLower();

            List<char> vowels = new List<char>
            {
                'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я'
            };

            List<char> consonants = new List<char>
            {
                'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л',
                'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ'
            };

            int vowelCount = 0;
            int consonantCount = 0;

            foreach (char c in lowerText)
            {
                if (vowels.Contains(c))
                {
                    vowelCount++;
                }
                else if (consonants.Contains(c))
                {
                    consonantCount++;
                }
            }

            Console.WriteLine($"Гласных: {vowelCount}, Согласных: {consonantCount}");
        }
    }
}