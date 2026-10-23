using System;

namespace Homework
{
    class Program
    {
        public static int[,] MatrixMaker(string[] array)
        {
            int[,] matrix = new int[2,2];
            byte firstOperator = 0;
            byte secondOperator = 0;
            foreach (string i in array)
            {
                matrix[secondOperator, firstOperator] = int.Parse(i);
                firstOperator += 1;
                if (firstOperator == 2) { firstOperator = 0; secondOperator += 1; }
                if (secondOperator == 2) secondOperator = 0;
            }
            return matrix;
        }
        public static void Main(string[] args)
        {
            int[,] firstMatrix = new int[2,2];
            int[,] secondMatrix = new int[2,2];
            Console.WriteLine("Введите значения элементов матрицы:");
            string[] firstArray = (Console.ReadLine() ?? "").Split();
            firstMatrix = MatrixMaker(firstArray);
            Console.WriteLine("Введите значения элементов матрицы:");
            string[] secondArray = (Console.ReadLine() ?? "").Split();
            secondMatrix = MatrixMaker(secondArray);
            foreach (int i in firstMatrix) {
                Console.WriteLine(i);
                    }
            foreach (int i in secondMatrix)
            {
                Console.WriteLine(i);
            }
            int[,] multiplication = {
            {firstMatrix[0,0] * secondMatrix[0,0] + firstMatrix[0,1] * secondMatrix[1,0],
                firstMatrix[0,0] * secondMatrix[0,1] + firstMatrix[0,1] * secondMatrix[1,1]},
            {firstMatrix[1,0] * secondMatrix[0,0] + firstMatrix[1,1] * secondMatrix[1,0],
                firstMatrix[1,0] * secondMatrix[0,1] + firstMatrix[1,1] * secondMatrix[1,1]}
            };
            foreach (int i in multiplication) {
                Console.WriteLine(i);
                    }
        }
    }
}