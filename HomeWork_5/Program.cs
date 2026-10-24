using System;
using System.IO;
using System.Reflection.Metadata;
namespace Homework

/// dotnet run C:\Users\515_n\Desktop\CSharp\HomeWork_5\HomeWork_5\Program.cs  test.txt
/// Для запуска программы
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
            else
            {
                string lowerText = content.ToLower();
                var vowels = new HashSet<char> { 'а', 'е', 'ё', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' };
                var consonants = new HashSet<char> { 'б', 'в', 'г', 'д', 'ж', 'з', 'й','к', 'л',
                 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ' };

                int vowelCount = lowerText.Count(c => vowels.Contains(c));
                int consonantCount = lowerText.Count(c => consonants.Contains(c));
                // Знаю, что лямбду ещё не использовали, но решил упростить задачу

                Console.WriteLine($"Гласных: {vowelCount}, Согласных: {consonantCount}");
            }

        }

    }
}